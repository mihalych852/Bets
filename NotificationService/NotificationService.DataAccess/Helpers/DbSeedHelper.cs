using Bets.Abstractions.DataAccess.EF.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NotificationService.Domain.Directories;

namespace NotificationService.DataAccess.Helpers
{
    public class DbSeedHelper
    {
        private readonly IConfiguration _configuration;
        private readonly DbContext _context;

        const string defaultCreatedBy = "DbSeedHelper";

        private readonly static Guid defaultMessengerId = Guid.Parse("2cd5dd28-3234-460f-a314-64e024e7f911");
        private readonly static string defaultMessengerName = "email";

        public static Guid MailMessengerId { get; private set; }

        public DbSeedHelper(IConfiguration configuration, DbContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        public async Task CreateDefaultMessengerAsync()
        {
            var dbSet = _context.Set<Messengers>();

            var defaultMessenger = dbSet.Where(x => x.Id == defaultMessengerId).FirstOrDefault();

            if (defaultMessenger != null)
            {
                MailMessengerId = defaultMessengerId;
                return;
            }
            else
            {
                defaultMessenger = dbSet.Where(x => x.Name == defaultMessengerName && x.DeletedDate == null).FirstOrDefault();
                if (defaultMessenger != null)
                {
                    MailMessengerId = defaultMessenger.Id;
                    return;
                }
                else
                {
                    var repo = new LaterDeletedEntityRepository<Messengers>(_context, _configuration);

                    defaultMessenger = new Messengers()
                    {
                        Id = defaultMessengerId,
                        Name = defaultMessengerName,
                        CreatedBy = defaultCreatedBy
                    };

                    await repo.AddAsync(defaultMessenger);
                }
            }
        }
    }
}

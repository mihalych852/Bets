using Bets.Abstractions.DataAccess.EF.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BetsService.DataAccess.Repositories
{
    public class EventsRepository : LaterDeletedEntityRepository<Domain.Events>
    {
        public EventsRepository(IDbContextFactory<DatabaseContext> contextFactory, IConfiguration config) : base(contextFactory.CreateDbContext(), config) { }

        /// <summary>
        /// Получение сущности по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор сущности</param>
        public override async Task<Domain.Events?> GetByIdAsync(Guid id)
        {
            return await _entitySet.Include("EventOutcomes").Where(x => x.Id == id).FirstOrDefaultAsync();
        }
    }
}

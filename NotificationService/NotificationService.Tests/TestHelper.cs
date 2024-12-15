using NotificationService.Models;
using NotificationService.Domain.Directories;

namespace NotificationService.Tests
{
    internal static class TestHelper
    {
        public static BettorAddressesRequest CreateBaseBettorAddressesRequest()
        {
            var bettorAddresses = new BettorAddressesRequest()
            {
                Address = "111@mail.ru",
                CreatedBy = "adminTest",
                Priority = 1,
                BettorId = Guid.Parse("113cf442-2597-4e53-a31d-a663f5239c1e"),
                MessengerId = Guid.Parse("071b7113-4b80-4f2a-ae17-15dce08de299")
            };

            return bettorAddresses;
        }

        public static BettorAddresses CreateBettorAddresses()
        {
            var bettorAddresses = new BettorAddresses()
            {
                Id = Guid.Parse("7d994823-8226-4273-b063-1a95f3cc1df8"),
                Address = "111@mail.ru",
                CreatedBy = "adminTest",
                Priority = 1,
                BettorId = Guid.Parse("113cf442-2597-4e53-a31d-a663f5239c1e"),
                Bettor = new Bettors()
                {
                    Id = Guid.Parse("113cf442-2597-4e53-a31d-a663f5239c1e"),
                    CreatedBy = "adminTest",
                    Nickname = "UnitTestUser"
                },
                MessengerId = Guid.Parse("071b7113-4b80-4f2a-ae17-15dce08de299"),
                Messenger = new Messengers()
                {
                    Id = Guid.Parse("071b7113-4b80-4f2a-ae17-15dce08de299"),
                    CreatedBy = "adminTest",
                    Name = "email"
                }
            };

            return bettorAddresses;
        }
    }
}

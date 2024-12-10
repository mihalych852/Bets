using NotificationService.Models;

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
    }
}

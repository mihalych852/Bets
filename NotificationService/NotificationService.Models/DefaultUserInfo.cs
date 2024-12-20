using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace NotificationService.Models
{
    public sealed class DefaultUserInfo
    {
        [JsonConstructor]
        [SetsRequiredMembers]
        public DefaultUserInfo(BettorAddressesRequest address, BettorRequest bettor)
        {
            this.Bettor = bettor;
            this.Address = address;
        }

        [SetsRequiredMembers]
        public DefaultUserInfo(Guid bettorId, string nickname, string email, string createdBy)
        {
            Address = new BettorAddressesRequest()
            {
                Priority = 1,
                Address = email,
                BettorId = bettorId,
                CreatedBy = createdBy,
                MessengerId = defaultMessengerId
            };
            Bettor = new BettorRequest()
            {
                Id = bettorId,
                Nickname = nickname,
                CreatedBy = createdBy
            };
        }

        public readonly static Guid defaultMessengerId = Guid.Parse("2cd5dd28-3234-460f-a314-64e024e7f911");

        public required BettorRequest Bettor { get; set; }
        public required BettorAddressesRequest Address { get; set; }
    }
}

using System.Diagnostics.CodeAnalysis;

namespace WalletService.Models
{
    public sealed class AddWalletRequest
    {
        [SetsRequiredMembers]
        public AddWalletRequest(Guid bettorId)
        {
            BettorId = bettorId;
        }

        public Guid BettorId { get; set; }
    }
}

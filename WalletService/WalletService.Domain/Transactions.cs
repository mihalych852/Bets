using Bets.Abstractions.Domain.DTO;

namespace WalletService.Domain
{
    /// <summary>
    /// Данные о транзакциях
    /// </summary>
    public class Transactions : CreatedEntity
    {
        /// <summary>
        /// Идентификатор игрока, для кошелька которого совершается транзакция
        /// </summary>
        public Guid BettorId { get; set; }

        /// <summary>
        /// Сумма транзакции
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Описание транзакции
        /// </summary>
        public required string Description { get; set; }
    }
}

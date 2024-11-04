namespace WalletService.Models
{
    public sealed class TransactionsRequest
    {
        /// <summary>
        /// Идентификатор игрока
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

        /// <summary>
        /// Создатель (идентификатор пользователя, сервиса и т.п.)
        /// </summary>
        public required string CreatedBy { get; set; }

    }
}

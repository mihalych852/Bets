
namespace WalletService.Models
{
    public sealed class CancelTransactionsRequest
    {
        /// <summary>
        /// Идентификатор отменяемой транзакции
        /// </summary>
        public Guid CanceledTransactionId { get; set; }

        /// <summary>
        /// Создатель (идентификатор пользователя, сервиса и т.п.)
        /// </summary>
        public required string CreatedBy { get; set; }
    }
}

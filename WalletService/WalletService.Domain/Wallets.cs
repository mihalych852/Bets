
namespace WalletService.Domain
{
    /// <summary>
    /// Кошелек
    /// </summary>
    public class Wallets
    {
        /// <summary>
        /// Идентификатор игрока - его же будем использовать в качестве идентификатора кошелька. 
        /// </summary>
        public Guid BettorId { get; set; }

        /// <summary>
        /// Текущий баланс
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Создатель (может быть идентификатором первой транзакции, а может быть и что-то другое)
        /// </summary>
        public required string CreatedBy { get; set; }
         
        /// <summary>
        /// Дата последнего изменения
        /// </summary>
        public DateTime ModifiedDate { get; set; }

        /// <summary>
        /// Кто последний раз изменял - идентификатор последней транзакции
        /// </summary>
        public Guid ModifiedBy { get; set; }
    }
}

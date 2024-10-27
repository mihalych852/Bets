
namespace BetsService.Domain
{
    public enum BetsState
    {
        /// <summary>
        /// Ставка сделана
        /// </summary>
        IsMade = 0,

        /// <summary>
        /// Ставка отменена
        /// </summary>
        Cancelled = 1,

        /// <summary>
        /// Ставка выиграла и ожидает выплаты
        /// </summary>
        AwaitingPayment = 2,

        /// <summary>
        /// Завершено
        /// </summary>
        Completed = 3,
    }
}

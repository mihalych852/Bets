using Bets.Abstractions.Domain.DTO;

namespace BetsService.Domain
{
    /// <summary>
    /// Ставка
    /// </summary>
    public class Bets : CreatedEntity
    {
        /// <summary>
        /// Статус ставки
        /// </summary>
        public BetsState State { get; set; }

        /// <summary>
        /// Игрок, делающий ставку
        /// </summary>
        public Guid BettorId { get; set; }

        /// <summary>
        /// Сумма ставки
        /// </summary>
        public decimal Amount { get; set; } = 100;

        /// <summary>
        /// Идентификатор события, на которое делается ставка
        /// </summary>
        public Guid EventOutcomesId { get; set; }

        /// <summary>
        /// Событие, на которое делается ставка
        /// </summary>
        public required virtual EventOutcomes EventOutcomes { get; set; }
    }
}

using BetsService.Models.Enums;

namespace BetsService.Models
{
    public sealed class BetsResponse
    {
        /// <summary>
        /// Идентификатор ставки
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Статус ставки
        /// </summary>
        public BetsState State { get; set; }

        /// <summary>
        /// Игрок, сделавший ставку
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
    }
}

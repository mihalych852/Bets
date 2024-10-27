
namespace BetsService.Models
{
    public sealed class EventOutcomeRequest
    {
        /// <summary>
        /// Описание исхода
        /// </summary>
        public required string Description { get; set; }

        /// <summary>
        /// Идентификатор события
        /// </summary>
        public Guid EventId { get; set; }

        /// <summary>
        /// Создатель
        /// </summary>
        public required string CreatedBy { get; set; }

        /// <summary>
        /// Комиссия со ставок на этот исход
        /// </summary>
        public decimal Commision { get; set; } = .025m;
    }
}

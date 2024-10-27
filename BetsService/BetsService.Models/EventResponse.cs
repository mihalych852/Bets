using BetsService.Models.Enums;

namespace BetsService.Models
{
    public sealed class EventResponse
    {
        /// <summary>
        /// Идентификатор события
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Описание события
        /// </summary>
        public required string Description { get; set; }

        /// <summary>
        /// Дата и время начала события
        /// </summary>
        public DateTime EventStartTime { get; set; }

        /// <summary>
        /// Дата и время окончания приема ставок на событие
        /// </summary>
        public DateTime BetsEndTime { get; set; }

        /// <summary>
        /// Статус события
        /// </summary>
        public EventsStatus Status { get; set; }

        /// <summary>
        /// Возможные исходы события
        /// </summary>
        public List<EventOutcomeResponse> EventOutcomes { get; set; } = [];
    }
}

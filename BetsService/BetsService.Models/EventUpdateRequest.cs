using BetsService.Models.Enums;

namespace BetsService.Models
{
    public sealed class EventUpdateRequest
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
        /// Кто последний раз изменял
        /// </summary>
        public string? ModifiedBy { get; set; }
    }
}

using BetsService.Domain;

namespace BetsService.DataAccess.DTO
{
    public sealed class BetsStateUpdateRequest
    {
        /// <summary>
        /// Идентификаторы ставок для обновления статуса
        /// </summary>
        public IEnumerable<Guid> Ids { get; set; } = Array.Empty<Guid>();

        /// <summary>
        /// Статус ставки
        /// </summary>
        public BetsState State { get; set; }
    }
}

using Bets.Abstractions.Domain.DTO;

namespace BetsService.Domain
{
    /// <summary>
    /// Исходы событий
    /// </summary>
    public class EventOutcomes : LaterDeletedEntity
    {
        /// <summary>
        /// Описание исхода
        /// </summary>
        public required string Description { get; set; }

        /// <summary>
        /// Коэф., на который надо домножить ставку для расчета выигрыша
        /// </summary>
        public decimal CurrentOdd { get; set; }

        /// <summary>
        /// Верно, если исход завершился успехом
        /// </summary>
        public bool? IsHappened { get; set; } = null;

        /// <summary>
        /// Верно, если событие закрыто или отменено
        /// </summary>
        public bool BetsClosed { get; set; } = false;  

        /// <summary>
        /// Идентификатор события
        /// </summary>
        public Guid EventId { get; set; }

        /// <summary>
        /// Событие, для которого возможен этот исход
        /// </summary>
        public required virtual Events Event { get; set; }

        /// <summary>
        /// Используется только для определения связи со ставками. 
        /// Нет смысла где-то выводить или ещё как-то использовать этот список.
        /// </summary>
        public List<Bets> Bets { get; } = [];

        /// <summary>
        /// Комиссия со ставок на этот исход
        /// </summary>
        public decimal Commision { get; set; } = .025m;
    }
}

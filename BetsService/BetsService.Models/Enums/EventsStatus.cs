
namespace BetsService.Models.Enums
{
    public enum EventsStatus
    {
        /// <summary>
        /// Ожидается
        /// </summary>
        Expected = 0,

        /// <summary>
        /// В процессе
        /// </summary>
        InProcess = 1,

        /// <summary>
        /// Завершено
        /// </summary>
        Completed = 2,

        /// <summary>
        /// Отменено
        /// </summary>
        Cancelled = 3
    }
}

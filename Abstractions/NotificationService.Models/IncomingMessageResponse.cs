using NotificationService.Models.Enums;

namespace NotificationService.Models
{
    public sealed class IncomingMessageResponse
    {
        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Идентификатор получателя сообщения (Bettors)
        /// </summary>
        public Guid TargetId { get; set; }

        /// <summary>
        /// Текст сообщения
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Тема сообщения
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Дата и время, до которого сообщение считается актуальным.
        /// После этой даты отправлять сообщение конечному получателю не имеет смысла.
        /// </summary>
        public DateTime? ActualDate { get; set; }

        /// <summary>
        /// Состояние отправки сообщения
        /// </summary>
        public SendingStates State { get; set; }
    }
}

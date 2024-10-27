using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetsService.Models
{
    public sealed class BetsRequest
    {
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
        /// Создатель
        /// </summary>
        public required string CreatedBy { get; set; }
    }
}

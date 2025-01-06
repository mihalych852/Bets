using Bets.Abstractions.DataAccess.EF.Repositories;
using BetsService.DataAccess.DTO;
using BetsService.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetsService.DataAccess.Repositories
{
    public class EventOutcomesRepository : LaterDeletedEntityRepository<EventOutcomes>
    {
        public EventOutcomesRepository(IDbContextFactory<DatabaseContext> contextFactory, IConfiguration config) : base(contextFactory.CreateDbContext(), config) { }


        /// <summary>
        /// Помечает исходы завершенных или отмененных событий
        /// </summary>
        /// <param name="ids">Идентификаторы помечаемых исходов</param>
        /// <returns>Количество обновленных</returns>
        public async Task<int> CloseAsync(IEnumerable<Guid> ids)
        {
            var updatedCount = await _entitySet.Where(x => ids.Contains(x.Id))
                .ExecuteUpdateAsync(u => u.SetProperty(p => p.BetsClosed, true));
            return updatedCount;
        }
    }
}

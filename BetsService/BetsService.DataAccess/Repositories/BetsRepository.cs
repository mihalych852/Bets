using Bets.Abstractions.DataAccess.EF.Repositories;
using BetsService.DataAccess.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BetsService.DataAccess.Repositories
{
    public class BetsRepository : CreatedEntityRepository<Domain.Bets>
    {
        public BetsRepository(IDbContextFactory<DatabaseContext> contextFactory, IConfiguration config) : base(contextFactory.CreateDbContext(), config) { }

        /// <summary>
        /// Получить все ставки конкретного игрока
        /// </summary>
        /// <param name="bettorId">Идентификатор игрока</param>
        public async Task<List<Domain.Bets>> GetListByBettorIdAsync(Guid bettorId)
        {
            return await _entitySet.Include("EventOutcomes").Where(x => x.BettorId == bettorId).ToListAsync();
        }

        /// <summary>
        /// Обновляет непосредственно статус для нескольких ставок
        /// </summary>
        /// <param name="request">Данные для обновления</param>
        /// <returns>Количество обновленных</returns>
        public async Task<int> UpdateStatesAsync(BetsStateUpdateRequest request)
        {
            var updatedCount = await _entitySet.Where(x => request.Ids.Contains(x.Id))
                .ExecuteUpdateAsync(u => u.SetProperty(p => p.State, request.State));
            return updatedCount;
        }

        /// <summary>
        /// Получить все ставки
        /// </summary>
        public async override Task<List<Domain.Bets>> GetAllAsync()
        {
            return await _entitySet.Include("EventOutcomes").ToListAsync();
        }

        /// <summary>
        /// Получение сущности по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор сущности</param>
        public override async Task<Domain.Bets?> GetByIdAsync(Guid id)
        {
            return await _entitySet.Include("EventOutcomes").Where(x => x.Id == id).FirstOrDefaultAsync();
        }
    }
}

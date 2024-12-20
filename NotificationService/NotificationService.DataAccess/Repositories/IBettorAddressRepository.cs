using Bets.Abstractions.Domain.Repositories.Interfaces;
using NotificationService.Domain.Directories;

namespace NotificationService.DataAccess.Repositories
{
    public interface IBettorAddressRepository : ICanDeleteEntitiesRepository<BettorAddresses>
        , IIdentifiableEntitiesRepository<BettorAddresses>
        , ICanUpdateEntitiesRepository<BettorAddresses>
        , ICanCreateEntitiesRepository<BettorAddresses>
        , IRepository<BettorAddresses>
    {
        Task AddRangeAsync(IEnumerable<BettorAddresses> entities);
        Task<List<BettorAddresses>> GetListByBettorIdAsync(Guid bettorId);
        Task<BettorAddresses?> GetByBettorIdWithMinPriorityAsync(Guid bettorId);
        Task<int> UpdateAddressAsync(BettorAddresses request);
        Task<int> UpdatePriorityAsync(BettorAddresses request);
        Task UpdateAsync(IEnumerable<BettorAddresses> entitys);

        //Task<int> DeleteAsync(DeleteRequest request);
        //Task<int> DeleteRangeAsync(DeleteListRequest request);
        //Task<BettorAddresses?> GetByIdAsync(Guid id);
        //Task<List<BettorAddresses>> GetAllAsync();
        //Task UpdateAsync(BettorAddresses entity);
        //Task<List<BettorAddresses>> GetListByIdsAsync(IEnumerable<Guid> ids);
        //Task AddAsync(BettorAddresses entity);
    }
}

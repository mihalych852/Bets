using Microsoft.EntityFrameworkCore;
using Bets.Abstractions.Domain.DTO;
using Bets.Abstractions.Domain.Repositories.Interfaces;
using Bets.Abstractions.Domain.Repositories.ModelRequests;
using System.Runtime.InteropServices.Marshalling;
using Microsoft.Extensions.Configuration;

namespace Bets.Abstractions.DataAccess.EF.Repositories
{
    /// <summary>
    /// Добавляет реализации методов отложенного удаления
    /// </summary>
    /// <typeparam name="T">Тип новой сущности</typeparam>
    public class LaterDeletedEntityRepository<T>
      : ModifiableEntityRepository<T>
      , ICanDeleteEntitiesRepository<T>
      , ICanUpdateEntitiesRepository<T>, ICanCreateEntitiesRepository<T>
      , IIdentifiableEntitiesRepository<T>, IRepository<T>
      where T : LaterDeletedEntity
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="context">Контекст хранилища</param>
        /// <param name="config">Конфигурация. 
        /// Необходима для определения значения _shouldSaveUniversalTimes
        /// , указывающего, стоит ли преобразовывать автоматически генерируемые перед сохранением даты и время в универсальные
        /// , чтобы избежать проблем с 'timestamp with time zone' при использовании Npgsql</param>
        public LaterDeletedEntityRepository(DbContext context, IConfiguration config) : base(context, config) { }

        /// <summary>
        /// Помечает сущность для удаления
        /// </summary>
        /// <param name="request">Идентификаторы удаляемых объектов и кем удаляются</param>
        /// <returns>Количество удаленных</returns>
        public async Task<int> DeleteAsync(DeleteRequest request)
        {
            var deletedCount = await _entitySet.Where(x => x.Id == request.Id && x.DeletedDate == null)
                .ExecuteUpdateAsync(u => u.SetProperty(p => p.DeletedDate, GetNow()).SetProperty(p => p.DeletedBy, request.DeletedBy));
            return deletedCount;  
        }

        /// <summary>
        /// Помечает несколько сущностей для удаления
        /// </summary>
        /// <param name="request">Идентификаторы удаляемых объектов и кем удаляются</param>
        /// <returns>Количество удаленных</returns>
        public async Task<int> DeleteRangeAsync(DeleteListRequest request)
        {
            var deletedCount = await _entitySet.Where(x => request.Ids.Contains(x.Id) && x.DeletedDate == null)
                .ExecuteUpdateAsync(u => u.SetProperty(p => p.DeletedDate, GetNow()).SetProperty(p => p.DeletedBy, request.DeletedBy));
            return deletedCount;   
        }

        /// <summary>
        /// Получение сущности по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор сущности</param>
        public override async Task<T?> GetByIdAsync(Guid id)
        {
            return await _entitySet.Where(x => x.Id == id && x.DeletedDate == null).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Получить все сущности типа T
        /// </summary>
        public override async Task<List<T>> GetAllAsync()
        {
            return await _entitySet.Where(x => x.DeletedDate == null).ToListAsync();
        }

        /// <summary>
        /// Получение списка сущностей по идентификаторам
        /// </summary>
        /// <param name="ids">Идентификаторы сущностей</param>
        public override async Task<List<T>> GetListByIdsAsync(IEnumerable<Guid> ids)
        {
            return await _entitySet.Where(x => ids.Contains(x.Id) && x.DeletedDate == null).ToListAsync();
        }
    }
}

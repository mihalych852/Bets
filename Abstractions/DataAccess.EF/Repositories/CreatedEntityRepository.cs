using Microsoft.EntityFrameworkCore;
using Bets.Abstractions.Domain.DTO;
using Bets.Abstractions.Domain.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Bets.Abstractions.DataAccess.EF.Repositories
{
    /// <summary>
    /// Добавляет реализации методов добавления данных в хранилище
    /// </summary>
    /// <typeparam name="T">Тип новой сущности</typeparam>
    public class CreatedEntityRepository<T> 
      : ReadingRepository<T>
      , ICanCreateEntitiesRepository<T>, IIdentifiableEntitiesRepository<T>, IRepository<T>
      where T : CreatedEntity
    {
        protected readonly bool _shouldSaveUniversalTimes;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="context">Контекст хранилища</param>
        /// <param name="config">Конфигурация. 
        /// Необходима для определения значения _shouldSaveUniversalTimes
        /// , указывающего, стоит ли преобразовывать автоматически генерируемые перед сохранением даты и время в универсальные
        /// , чтобы избежать проблем с 'timestamp with time zone' при использовании Npgsql</param>
        public CreatedEntityRepository(DbContext context, IConfiguration config) : base(context) 
        {
            var shouldSaveUniversalTimes = config.GetSection("ShouldSaveUniversalTimes").Value;
            if(!bool.TryParse(shouldSaveUniversalTimes, out _shouldSaveUniversalTimes))
            {
                _shouldSaveUniversalTimes = false;
            }
        }

        /// <summary>
        /// Добавляет новую сущность в хранилище
        /// </summary>
        /// <param name="entity">Новые данные</param>
        public virtual async Task AddAsync(T entity)
        {
            entity.CreatedDate = GetNow();
            await _entitySet.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Добавляет несколько сущностей в хранилище
        /// </summary>
        /// <param name="entity">Новые данные</param>
        public virtual async Task AddRangeAsync(IEnumerable<T> entities)
        {
            _dbContext.ChangeTracker.AutoDetectChangesEnabled = false;
            foreach (var entity in entities)
            {
                entity.CreatedDate = GetNow();
            }
            await _entitySet.AddRangeAsync(entities);
            await _dbContext.SaveChangesAsync();
            _dbContext.ChangeTracker.AutoDetectChangesEnabled = true;
        }

        protected DateTime GetNow()
        {
            if (_shouldSaveUniversalTimes)
            {
                return DateTime.Now.ToUniversalTime();
            }
            else
            {
                return DateTime.Now;
            }
        }
    }
}

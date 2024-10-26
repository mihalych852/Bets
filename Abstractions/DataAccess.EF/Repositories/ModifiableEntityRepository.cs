using Microsoft.EntityFrameworkCore;
using Bets.Abstractions.Domain.DTO;
using Bets.Abstractions.Domain.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Bets.Abstractions.DataAccess.EF.Repositories
{
    /// <summary>
    /// Добавляет реализации методов модификации данных в хранилище
    /// </summary>
    /// <typeparam name="T">Тип новой сущности</typeparam>
    public class ModifiableEntityRepository<T>
      : CreatedEntityRepository<T>
      , ICanUpdateEntitiesRepository<T>, ICanCreateEntitiesRepository<T>
      , IIdentifiableEntitiesRepository<T>, IRepository<T>
      where T : ModifiableEntity
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="context">Контекст хранилища</param>
        /// <param name="config">Конфигурация. 
        /// Необходима для определения значения _shouldSaveUniversalTimes
        /// , указывающего, стоит ли преобразовывать автоматически генерируемые перед сохранением даты и время в универсальные
        /// , чтобы избежать проблем с 'timestamp with time zone' при использовании Npgsql</param>
        public ModifiableEntityRepository(DbContext context, IConfiguration config) : base(context, config) { }

        /// <summary>
        /// Изменяет в хранилище информацию о сущности
        /// </summary>
        /// <param name="entity">Изменяемые данные</param>
        public virtual async Task UpdateAsync(T entity)
        {
            entity.ModifiedDate = GetNow();
            _entitySet.Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Изменяет в хранилище информацию о нескольких сущностях
        /// </summary>
        /// <param name="entity">Изменяемые данные</param>
        public virtual async Task UpdateAsync(IEnumerable<T> entitys)
        {
            var modifiedDate = GetNow();
            foreach (var entity in entitys)
            {
                entity.ModifiedDate = modifiedDate;
                _entitySet.Update(entity);
            }
            await _dbContext.SaveChangesAsync();
        }
    }
}

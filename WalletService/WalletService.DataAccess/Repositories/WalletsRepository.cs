using Microsoft.EntityFrameworkCore;
//using MongoDB.Driver;
//using MongoDB.Driver.Linq;
using WalletService.Domain;

namespace WalletService.DataAccess.Repositories
{
    public class WalletsRepository
    {
        private readonly DbContext _dataContext;
        //private readonly IMongoCollection<Wallets> _collection;

        public WalletsRepository(DbContext dataContext)
        {
            _dataContext = dataContext;
            //_collection = dataContext.Set<Wallets>();
        }

        //private readonly IMongoCollection<Wallets> _collection;

        //public WalletsRepository(IMongoDatabase database, string collectionName)
        //{
        //    _collection = database.GetCollection<Wallets>(collectionName);
        //}

        //public async Task<IEnumerable<Wallets>> GetAllAsync()
        //{   
        //    return await _collection.Find(Builders<Wallets>.Filter.Empty).ToListAsync();
        //}

        public async Task<Wallets?> GetByIdAsync(Guid id)
        {
            var entities = await _dataContext.Set<Wallets>().FirstOrDefaultAsync(x => x.BettorId == id);

            return entities;

            //return _collection.Find(Builders<Wallets>.Filter.Eq(e => e.BettorId, id)).FirstOrDefaultAsync();
        }

        public async Task AddAsync(Wallets entity)
        {
            await _dataContext.Set<Wallets>().AddAsync(entity);
            await _dataContext.SaveChangesAsync();

            //await _collection.InsertOneAsync(entity);
            //return entity;
        }

        public async Task UpdateAsync(Guid id, Wallets entity)
        {
            await _dataContext.SaveChangesAsync();

            //await _collection.ReplaceOneAsync(Builders<Wallets>.Filter.Eq(e => e.BettorId, id), entity);
        }
    }
}

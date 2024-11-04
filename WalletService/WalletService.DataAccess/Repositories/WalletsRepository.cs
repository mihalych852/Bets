using MongoDB.Driver;
using WalletService.Domain;

namespace WalletService.DataAccess.Repositories
{
    public class WalletsRepository
    {
        private readonly IMongoCollection<Wallets> _collection;

        public WalletsRepository(IMongoDatabase database, string collectionName)
        {
            _collection = database.GetCollection<Wallets>(collectionName);
        }

        //public async Task<IEnumerable<Wallets>> GetAllAsync()
        //{   
        //    return await _collection.Find(Builders<Wallets>.Filter.Empty).ToListAsync();
        //}

        public async Task<Wallets> GetByIdAsync(Guid id)
        {
            return await _collection.Find(Builders<Wallets>.Filter.Eq(e => e.BettorId, id)).FirstOrDefaultAsync();
        }

        public async Task<Wallets> AddAsync(Wallets entity)
        {
            await _collection.InsertOneAsync(entity);
            return entity;
        }

        public async Task UpdateAsync(Guid id, Wallets entity)
        {
            await _collection.ReplaceOneAsync(Builders<Wallets>.Filter.Eq(e => e.BettorId, id), entity);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using WalletService.Domain;

namespace WalletService.DataAccess
{
    public class MongoDBContext : DbContext
    {
        private readonly IMongoDatabase _database;
        private readonly MongoClient _client;
        private readonly string _dbName;

        public MongoDBContext(IOptions<MongoDBSettings> mongoDBSettings)
        {
            _client = new MongoClient(mongoDBSettings.Value.Connection);
            _dbName = mongoDBSettings.Value.DatabaseName;
            _database = _client.GetDatabase(_dbName);
        }

        public IMongoDatabase Database => _database;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseMongoDB(_client, _dbName);

        public DbSet<Transactions> Transactions => Set<Transactions>();
        public DbSet<Wallets> Wallets => Set<Wallets>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Transactions>().HasKey(e => e.Id);

            modelBuilder.Entity<Wallets>().HasKey(e => e.BettorId);
            modelBuilder.Entity<Wallets>(builder =>
                builder.Property(entity => entity.BettorId).HasElementName("_id"));
        }
    }
}

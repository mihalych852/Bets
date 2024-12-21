using MassTransit.EntityFrameworkCoreIntegration;
using Microsoft.EntityFrameworkCore;
using Sagas.Helpers;

namespace Sagas
{
    public sealed class SagasDbContext : SagaDbContext
    {
        public SagasDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override IEnumerable<ISagaClassMap> Configurations =>
        [
            new MakeBetsSagaStateMap()
        ];
    }
}

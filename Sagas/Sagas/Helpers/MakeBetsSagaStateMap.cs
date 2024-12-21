using MassTransit;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Sagas.States;

namespace Sagas.Helpers
{
    public class MakeBetsSagaStateMap : SagaClassMap<MakeBetsSagaState>
    {
        protected override void Configure(EntityTypeBuilder<MakeBetsSagaState> entity, ModelBuilder model)
        {
            base.Configure(entity, model);
            entity.Property(x => x.CurrentState).HasMaxLength(255);
        }
    }
}

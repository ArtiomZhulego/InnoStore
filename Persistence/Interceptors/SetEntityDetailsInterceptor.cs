using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Interceptors;

public class SetEntityDetailsInterceptor : BaseChangedInterceptor
{
    protected override void OnSavingChangesAction(DbContext context)
    {
        var timestamp = DateTime.UtcNow.ToUniversalTime();
        foreach (var entry in context.ChangeTracker.Entries<BaseEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedAt = timestamp;
                    break;
                case EntityState.Modified:
                    entry.Entity.UpdatedAt = timestamp;
                    break;
                default: break;
            }
        }
    }
}

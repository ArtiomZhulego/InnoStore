using Domain.Entities;
using Domain.ValueModels;
using Microsoft.EntityFrameworkCore;
using Persistence.DataInitializers.Abstractions;

namespace Persistence.DataInitializers;

public class PassedEventCostInitializer(InnoStoreContext context) : IDataInitializer
{
    public async Task InitializeAsync(CancellationToken cancellationToken = default)
    {
        var areAnyCosts = await context.PassedEventCosts.AnyAsync(cancellationToken);

        if (areAnyCosts)
        {
            return;
        }

        var passedEventCosts = new[]
        {
            new PassedEventCost
            {
                EventType = PassedEventType.OpenMeetUp,
                Amount = 10,
            },
            new PassedEventCost
            {
                EventType = PassedEventType.InternalMeetUp,
                Amount = 5,
            },
            new PassedEventCost
            {
                EventType = PassedEventType.UniversityMeetUp,
                Amount = 3,
            }
        };

        await context.PassedEventCosts.AddRangeAsync(passedEventCosts);
        await context.SaveChangesAsync(cancellationToken);
    }
}

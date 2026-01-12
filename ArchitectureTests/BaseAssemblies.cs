using System.Reflection;

namespace ArchitectureTests;

public abstract class BaseAssemblies
{
    protected static readonly Assembly DomainAssembly = typeof(Domain.Entities.User).Assembly; 
    protected static readonly Assembly ApplicationAssembly = typeof(Application.Extensions.ServiceCollectionExtension).Assembly; 
    protected static readonly Assembly PresentationAssembly = typeof(Presentation.Controllers.HealthController).Assembly; 
    protected static readonly Assembly PersistenceAssembly = typeof(Persistence.InnoStoreContext).Assembly;
    protected static readonly Assembly AbstractionAssembly = typeof(Application.Abstractions.DTOs.Entities.UserDTO).Assembly;
}
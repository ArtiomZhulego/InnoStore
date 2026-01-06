using System.Net.Mime;
using NetArchTest.Rules;

namespace ArchitectureTests;

public sealed class ProjectCommunicationTests : BaseAssemblies
{
    [Fact]
    public void Domain_Should_Not_Depend_On_Any_Other_Project()
    {
        var result = Types.InAssembly(DomainAssembly)
            .ShouldNot()
            .HaveDependencyOnAny(
                "Persistence",
                "Application",
                "Application.Abstractions",
                "Presentation",
                "InnoStore")
            .GetResult();

        Assert.True(result.IsSuccessful, "Domain layer should not depend on any other layer");
    }

    [Fact]
    public void Persistence_Should_Only_Depend_On_Domain()
    {
        var result = Types.InAssembly(PersistenceAssembly)
            .ShouldNot()
            .HaveDependencyOnAny(
                "Application",
                "Application.Abstractions",
                "Presentation",
                "InnoStore")
            .GetResult();

        Assert.True(result.IsSuccessful, "Persistence layer should only depend on Domain");
    }

    [Fact]
    public void ApplicationAbstractions_Should_Not_Depend_On_Any_Other_Project()
    {
        var result = Types.InAssembly(AbstractionAssembly)
            .ShouldNot()
            .HaveDependencyOnAny(
                "Domain",
                "Persistence",
                "Application",
                "Presentation",
                "InnoStore")
            .GetResult();

        Assert.True(result.IsSuccessful, "Application.Abstractions should not depend on any other layer");
    }

    [Fact]
    public void Application_Should_Only_Depend_On_Domain_And_ApplicationAbstractions()
    {
        var result = Types.InAssembly(ApplicationAssembly)
            .ShouldNot()
            .HaveDependencyOnAny("Presentation", "Persistence")
            .GetResult();

        Assert.True(result.IsSuccessful, "Application layer should only depend on Domain and Application.Abstractions");
    }

    [Fact]
    public void Presentation_Should_Only_Depend_On_ApplicationAbstractions_And_Web()
    {
        var result = Types.InAssembly(PresentationAssembly)
            .ShouldNot().HaveDependencyOnAny("Application", "Domain", "Persistence")
            .GetResult();

        Assert.True(result.IsSuccessful, "Presentation layer should only depend on Application.Abstractions and Web");
    }
}
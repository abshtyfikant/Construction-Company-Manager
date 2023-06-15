using System.Reflection;
using NetArchTest.Rules;

namespace Tests;

public class ArchitectureTests
{
    private const string DomainNamespace = "Domain";
    private const string ApplicationNamespace = "Application";
    private const string InfrastructureNamespace = "Infrastructure";
    private const string PresentationNamespace = "WebApi";

    [Fact]
    public void DomainLayer_DoesNotHaveDependencies()
    {
        // Arrange
        var assembly = Assembly.Load(DomainNamespace);

        var otherProjects = new[]
        {
            PresentationNamespace,
            InfrastructureNamespace,
            ApplicationNamespace
        };

        // Act
        var testResult = Types
            .InAssembly(assembly)
            .ShouldNot()
            .NotHaveDependencyOnAll(otherProjects)
            .GetResult();

        // Assert
        testResult.IsSuccessful.Equals(true);
    }

    [Fact]
    public void ApplicationLayer_DoesNotHaveDependencies()
    {
        // Arrange
        var assembly = Assembly.Load(ApplicationNamespace);

        var otherProjects = new[]
        {
            PresentationNamespace,
            InfrastructureNamespace
        };

        // Act
        var testResult = Types
            .InAssembly(assembly)
            .ShouldNot()
            .NotHaveDependencyOnAll(otherProjects)
            .GetResult();

        // Assert
        testResult.IsSuccessful.Equals(true);
    }

    [Fact]
    public void InfrastructureLayer_DoesNotHaveDependencies()
    {
        // Arrange
        var assembly = Assembly.Load(InfrastructureNamespace);

        var otherProjects = new[]
        {
            PresentationNamespace
        };

        // Act
        var testResult = Types
            .InAssembly(assembly)
            .ShouldNot()
            .NotHaveDependencyOnAll(otherProjects)
            .GetResult();

        // Assert
        testResult.IsSuccessful.Equals(true);
    }

    [Fact]
    public void PresentationLayer_DoesNotHaveDependencies()
    {
        // Arrange
        var assembly = Assembly.Load(PresentationNamespace);

        var otherProjects = new[]
        {
            DomainNamespace
        };

        // Act
        var testResult = Types
            .InAssembly(assembly)
            .ShouldNot()
            .NotHaveDependencyOnAll(otherProjects)
            .GetResult();

        // Assert
        testResult.IsSuccessful.Equals(true);
    }
}
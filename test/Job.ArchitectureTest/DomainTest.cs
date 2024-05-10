using System.Reflection;
using FluentAssertions;
using NetArchTest.Rules;

namespace Job.ArchitectureTest;

[Trait("Architecture", "Domain")]
public sealed class DomainTest
{
    private readonly Assembly _domain = Assembly.Load("Job.Domain");

    [Fact]
    public void ShouldHaveClassesSealedOrAbstractSearchForDomain()
    {
        var result = Types
            .InAssembly(_domain)
            .That()
            .AreClasses()
            .Should()
            .BeSealed()
            .Or()
            .BeAbstract()
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void DomainShouldNotHaveDependency()
    {
        var result = Types
            .InAssembly(_domain)
            .ShouldNot()
            .HaveDependencyOnAny("Job.WebApi", "Job.Infrastructure")
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }
}
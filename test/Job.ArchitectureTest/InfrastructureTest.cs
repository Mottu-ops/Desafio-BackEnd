using System.Reflection;
using FluentAssertions;
using NetArchTest.Rules;

namespace Job.ArchitectureTest;

[Trait("Architecture", "Infrastructure")]
public class InfrastructureTest
{
    private readonly Assembly _infrastructure = Assembly.Load("Job.Infrastructure");

    [Fact]
    public void DependencyDomainShouldHaveClasses()
    {
        var result = Types
            .InAssembly(_infrastructure)
            .That()
            .HaveDependencyOn("Job.Domain")
            .Should()
            .BeClasses()
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void DependencyWebApiShouldNotHaveClasses()
    {
        var result = Types
            .InAssembly(_infrastructure)
            .ShouldNot()
            .HaveDependencyOnAny("Job.WebApi")
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void ClassesHaveNameEndingWithRepositoryHaveDependencyDomain()
    {
        var result = Types
            .InAssembly(_infrastructure)
            .That()
            .HaveNameEndingWith("Repository")
            .Should()
            .HaveDependencyOn("Job.Domain")
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }
}
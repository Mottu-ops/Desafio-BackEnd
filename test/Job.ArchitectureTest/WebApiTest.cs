using System.Reflection;
using FluentAssertions;
using NetArchTest.Rules;

namespace Job.ArchitectureTest;

[Trait("Architecture", "WebApi")]
public class WebApiTest
{
    private readonly Assembly _webApi = Assembly.Load("Job.WebApi");

    [Fact]
    public void ControllerShouldNotReferenceRepositories()
    {
        var result = Types
            .InAssembly(_webApi)
            .That()
            .HaveNameEndingWith("Controller")
            .ShouldNot()
            .HaveDependencyOn("Job.Infrastructure")
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }
}
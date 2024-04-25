using Job.Domain.Repositories;
using Job.Domain.Services;
using Microsoft.Extensions.Logging;
using Moq;

namespace Job.UnitTests.Domain.Services;

public class MotoboyServiceTest
{
    private readonly Mock<ILogger<MotoboyService>> _logger = new();
    private readonly Mock<IMotoboyRepository> _managerRepository = new();
    private readonly MotoboyService _motoboyService;

    public MotoboyServiceTest()
    {
        _motoboyService = new MotoboyService(_logger.Object, _managerRepository.Object);
    }

}
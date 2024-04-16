using Motorent.Application.Common.Abstractions.Requests;

namespace Motorent.Application.UnitTests.TestUtils.Dummy;

public record DummyTransactionalRequest : IRequest<Result<object>>, ITransactional;
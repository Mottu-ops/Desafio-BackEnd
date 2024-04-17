using Motorent.Application.Common.Abstractions.Requests;

namespace Motorent.TestUtils.Dummies;

public record DummyTransactionalRequest : IRequest<Result<object>>, ITransactional;
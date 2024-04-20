namespace Motorent.TestUtils.Dummies;

public sealed class DummyOutboxEntity
{
    public Guid Id { get; init; } = Guid.NewGuid();
}
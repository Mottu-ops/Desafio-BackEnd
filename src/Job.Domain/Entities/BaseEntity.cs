namespace Job.Domain.Entities;

public abstract class BaseEntity
{
    public Guid Id { get; private set; } = Guid.NewGuid();

    public DateTime Created { get; private set; } = DateTime.Now;

    public DateTime? Updated { get; private set; }

    protected void Update()
    {
        Updated = DateTime.Now;
    }
}
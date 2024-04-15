namespace Motorent.Domain.Common.Entities;

public interface IAuditable
{
    public DateTimeOffset CreatedAt { get; }
    
    public string CreatedBy { get; }
    
    public DateTimeOffset? UpdatedAt { get; }
    
    public string? UpdatedBy { get;  }
    
    public void SetCreated(DateTimeOffset time, string createdBy);
    
    public void SetUpdated(DateTimeOffset time, string updatedBy);
}
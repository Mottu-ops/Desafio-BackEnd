using System;

namespace MT.Backend.Challenge.Domain.Entities
{
    public class BaseEntity
    {
        public string Id { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public bool Active { get; set; } = true;
    }
}

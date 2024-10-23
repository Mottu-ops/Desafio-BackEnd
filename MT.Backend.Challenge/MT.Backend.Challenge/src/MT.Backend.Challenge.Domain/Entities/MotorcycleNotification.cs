using System;
using System.Collections.Generic;
using System.Text;

namespace MT.Backend.Challenge.Domain.Entities
{
    public class MotorcycleNotification
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string MotorcycleId { get; set; }
        public string Message { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}

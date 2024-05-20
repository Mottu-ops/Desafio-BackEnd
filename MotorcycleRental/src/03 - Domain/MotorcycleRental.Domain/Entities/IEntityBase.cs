namespace MotorcycleRental.Domain.Entities
{
    public interface IEntityBase
    {
        public Guid Id { get; }
        public DateTime CreatedAt { get; }
        public bool IsActived { get; }
    }
}

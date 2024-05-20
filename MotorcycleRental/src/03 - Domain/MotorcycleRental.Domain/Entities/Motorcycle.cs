using MotorcycleRental.Domain.Events;

namespace MotorcycleRental.Domain.Entities
{
    public class Motorcycle : AggregateRoot
    {
        protected Motorcycle() { }
        public Motorcycle(int year, string model, string plate)
        {
            Year = year;
            Model = model;
            Plate = plate;
            IsActived = true;
        }

        public int Year { get; private set; }
        public string Model { get; private set; }
        public string Plate { get; private set; }
        public ICollection<RentalContract> RentalContracts { get; set; }

        public void SetCreatedAtDate()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
            AddEvent(new MotorcycleCreated(Id, Year, Model, Plate, CreatedAt, IsActived));
        }
    }
}

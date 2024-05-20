namespace MotorcycleRental.Domain.Entities.Mongo
{
    public class MotorcycleMgDb
    {
        protected MotorcycleMgDb() { }
        public MotorcycleMgDb(Guid id, int year, string model, string plate, DateTime createdAt, bool isActived)
        {
            Id = id;
            Year = year;
            Model = model;
            Plate = plate;
            CreatedAt = createdAt;
            IsActived = isActived;
        }

        public Guid Id { get; private set; }
        public int Year { get; private set; }
        public string Model { get; private set; }
        public string Plate { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public bool IsActived { get; private set; }
    }
}

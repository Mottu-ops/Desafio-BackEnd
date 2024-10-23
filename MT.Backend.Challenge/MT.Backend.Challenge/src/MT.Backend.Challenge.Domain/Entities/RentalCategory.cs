
namespace MT.Backend.Challenge.Domain.Entities
{
    public class RentalCategory : BaseEntity
    {
        public string Name { get; set; } = null!;
        public int RentalCategoryDays { get; set; }
        public decimal Price { get; set; }
        public decimal PercentualFine { get; set; }
    }

}

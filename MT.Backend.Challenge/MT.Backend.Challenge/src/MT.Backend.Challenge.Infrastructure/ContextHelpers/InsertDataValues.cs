using Microsoft.EntityFrameworkCore;
using MT.Backend.Challenge.Domain.Entities;

namespace MT.Backend.Challenge.Infrastructure.ContextHelpers
{
    public static class InsertDataValues
    {
        public static void InsertDataRentalCategoty(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RentalCategory>().HasData(
                new RentalCategory
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "7 dias",
                    RentalCategoryDays = 7,
                    Price = 30,
                    PercentualFine = 0.2m
                },
                new RentalCategory
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "15 dias",
                    RentalCategoryDays = 15,
                    Price = 28,
                    PercentualFine = 0.4m
                },
                new RentalCategory
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "30 dias",
                    RentalCategoryDays = 30,
                    Price = 22,
                    PercentualFine = 0
                },
                new RentalCategory
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "45 dias",
                    RentalCategoryDays = 45,
                    Price = 20,
                    PercentualFine = 0
                },
                new RentalCategory
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "50 dias",
                    RentalCategoryDays = 50,
                    Price = 18,
                    PercentualFine = 0
                }
            );
        }
    }
}

using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using MotorcycleRental.DeliveryManagementService.Service.DTOs;
using MotorcycleRental.DeliveryManagementService.Service.DTOs.Validations;
using MotorcycleRental.DeliveryManagementService.Service.Mappings;
using MotorcycleRental.DeliveryManagementService.Service.Services.DeliverymanService;
using MotorcycleRental.DeliveryManagementService.Service.Services.RentalContractService;

namespace MotorcycleRental.DeliveryManagementService.Service
{
    public static class ApplicationModule
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {

            services.AddAutoMapper(typeof(DomainToDtoProfile));
            services.AddValidations();
            services.AddService();
            return services;
        }

        private static IServiceCollection AddService(this IServiceCollection services)
        {
            services.AddScoped<IDeliverymanService, DeliverymanService>();
            services.AddScoped<IRentalContractService, RentalContractService>();
            return services;
        }
        private static IServiceCollection AddValidations(this IServiceCollection services)
        {
            //Deliveryman
            services.AddTransient<IValidator<DeliverymanAddDto>, DeliverymanAddDtoValidation>();
            services.AddTransient<IValidator<DeliverymanFullDto>, DeliverymanFullDtoValidation>();
            services.AddTransient<IValidator<SimulationPeriodDto>, SimulationPeriodDtoValidation>();
            services.AddTransient<IValidator<RentalContractAddDto>, RentalContractAddDtoValidation>();
            services.AddTransient<IValidator<RentalContractFullDto>, RentalContractFullDtoValidation>();
            services.AddTransient<IValidator<CheckRentalPriceInputDto>, CheckRentalPriceInputDtoValidation>();
            return services;
        }
    }
}

using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using MotorcycleRental.AdminManagementService.Service.UseCases.MotorcycleUseCase;
using MotorcycleRental.AdminManagementService.Service.UseCases.MotorcycleUseCase.GetAllMotorcycle;
using MotorcycleRental.AdminManagementService.Service.UseCases.MotorcycleUseCase.Validations;
using MotorcycleRental.AdminManagementService.Service.UseCases.RentalPlanUseCase.DeleteRentalPlan;
using MotorcycleRental.AdminManagementService.Service.UseCases.RentalPlanUseCase.GetAllRentalPlan;
using MotorcycleRental.AdminManagementService.Service.UseCases.RentalPlanUseCase.GetRentalPlanById;
using MotorcycleRental.AdminManagementService.Service.UseCases.RentalPlanUseCase.UpdateRentalPlan;
using MotorcycleRental.AdminManagementService.Service.UseCases.RentalPlanUseCase.Validations;


namespace MotorcycleRental.AdminManagementService.Service
{
    public static class ApplicationModule
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddUseCases();
            services.AddValidations();
            services.AddAutoMapper(typeof(DomainToUseCaseProfile));
            return services;
        }

        private static IServiceCollection AddUseCases(this IServiceCollection services)
        {
            //Motorcycle
            services.AddScoped<IAddMotorcycleUseCase, AddMotorcycleUseCase>();
            services.AddScoped<IGetMotorcycleByPlateUseCase, GetMotorcycleByPlateUseCase>();
            services.AddScoped<IGetAllMotorcycleUseCase, GetAllMotorcycleUseCase>();
            services.AddScoped<IUpdateMotorcycleUseCase, UpdateMotorcycleUseCase>();
            services.AddScoped<IDeleteMotorcycleUseCase, DeleteMotorcycleUseCase>();
            //RentalPlan
            services.AddScoped<IAddRentalPlanUseCase, AddRentalPlanUseCase>();
            services.AddScoped<IGetAllRentalPlanUseCase, GetAllRentalPlanUseCase>();
            services.AddScoped<IGetRentalPlanByIdUseCase, GetRentalPlanByIdUseCase>();
            services.AddScoped<IUpdateRentalPlanUseCase, UpdateRentalPlanUseCase>();
            services.AddScoped<IDeleteRentalPlanUseCase, DeleteRentalPlanUseCase>();
            return services;
        }

        private static IServiceCollection AddValidations(this IServiceCollection services)
        {
            //Motorcycle
            services.AddTransient<IValidator<AddMotorcycleInput>, AddMotorcycleInputValidation>();
            services.AddTransient<IValidator<MotorcycleInputOutput>, UpdateMotorcycleInputValidation>();
            services.AddTransient<IValidator<DeleteMotorcycleInput>, DeleteMotorcycleInputValidation>();
            services.AddTransient<IValidator<GetMotorcycleByPlateInput>, GetMotorcycleByPlateInputValidation>();

            //Rental Plan
            services.AddTransient<IValidator<AddRentalPlanInput>, AddRentalPlanInputValidation>();
            services.AddTransient<IValidator<UpdateRentalPlanIntput>, UpdateRentalPlanValidation>();
            return services;
        }
    }
}

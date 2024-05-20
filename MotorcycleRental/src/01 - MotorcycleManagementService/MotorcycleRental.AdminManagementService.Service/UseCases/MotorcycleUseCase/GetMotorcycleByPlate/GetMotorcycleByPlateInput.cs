namespace MotorcycleRental.AdminManagementService.Service.UseCases.MotorcycleUseCase.GetMotorcycleByPlate
{
    public class GetMotorcycleByPlateInput
    {
        public GetMotorcycleByPlateInput(string plate)
        {
            Plate = plate;
        }

        public string Plate { get; set; }
    }
}

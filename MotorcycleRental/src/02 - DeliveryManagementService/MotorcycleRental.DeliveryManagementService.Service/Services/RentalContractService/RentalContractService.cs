using AutoMapper;
using MotorcycleRental.DeliveryManagementService.Service.DTOs;
using MotorcycleRental.DeliveryManagementService.Service.Exceptions;
using MotorcycleRental.Domain.Entities;
using MotorcycleRental.Domain.Interfaces;

namespace MotorcycleRental.DeliveryManagementService.Service.Services.RentalContractService
{
    public class RentalContractService : IRentalContractService
    {
        private readonly IRentalContractRepository _rentalContractRepository;
        private readonly IRentalPlanRepository _rentalPlanRepository;
        private readonly IDeliverymanRepository _deliverymanRepository;
        private readonly IMotorcycleRepository _motorcycleRepository;
        private readonly IMapper _mapper;

        public RentalContractService(IMapper mapper,
                                     IRentalPlanRepository rentalPlanRepository,
                                     IRentalContractRepository rentalContractRepository,
                                     IDeliverymanRepository deliverymanRepository,
                                     IMotorcycleRepository motorcycleRepository)
        {
            _mapper = mapper;
            _rentalPlanRepository = rentalPlanRepository;
            _deliverymanRepository = deliverymanRepository;
            _rentalContractRepository = rentalContractRepository;
            _motorcycleRepository = motorcycleRepository;
        }

        public async Task AddAsync(RentalContractAddDto rentalContractDto)
        {
            var plan = await _rentalPlanRepository.GetByIdAsync(rentalContractDto.RentanPlanId);

            if (plan == null)
                throw new NotFoundException("Rental plan not found!");

            var deliveryman = await _deliverymanRepository.GetByIdAsync(rentalContractDto.DeliverymanId);

            if (deliveryman == null)
                throw new NotFoundException("Deliveryman not found!");

            var motorcycle = await _motorcycleRepository.GetByIdAsync(rentalContractDto.MotorcycleId);

            if (motorcycle == null)
                throw new NotFoundException("Motorcycle not found!");

            RentalContract rentalContract = _mapper.Map<RentalContract>(rentalContractDto);

            rentalContract.SetRentalValue(plan.Days * plan.DayValue);
            rentalContract.SetExpectedEndDate(rentalContract.StartDate.AddDays(plan.Days));

            await _rentalContractRepository.AddAsync(rentalContract);
        }

        public async Task DeleteAsync(Guid id)
        {
            var rentalContract = await _rentalContractRepository.GetByIdAsync(id);

            if (rentalContract == null)
                throw new NotFoundException("Rental Contract not found!");

            await _rentalContractRepository.DeleteAsync(rentalContract);
        }

        public async Task<List<RentalContractFullDto>> GetAllAsync()
        {
            var rentalContract = await _rentalContractRepository.GetAllAsync();

            if (rentalContract == null)
                return new List<RentalContractFullDto>();

            List<RentalContractFullDto> list = _mapper.Map<List<RentalContractFullDto>>(rentalContract);

            return list;
        }

        public async Task<RentalContractFullDto> GetByIdAsync(Guid id)
        {
            RentalContract rentalContract = await _rentalContractRepository.GetByIdAsync(id);

            if (rentalContract == null)
                throw new NotFoundException("Rental Contract not found!");

            RentalContractFullDto rentalContractDto = _mapper.Map<RentalContractFullDto>(rentalContract);

            return rentalContractDto;

        }
        public async Task UpdateAsync(RentalContractFullDto rentalContractDto)
        {
            RentalContract rentalContract = _mapper.Map<RentalContract>(rentalContractDto);

            rentalContract.SetUpdateDate();
            await _rentalContractRepository.UpdateAsync(rentalContract);
        }

        public async Task<CheckTotalRentalPriceDto> CheckTotalRentalPrice(Guid contractId, DateTime endDate)
        {
            RentalContract contract = await _rentalContractRepository.GetByIdAsync(contractId);

            if (contract == null)
                throw new NotFoundException("Rental Contract not found!");

            RentalPlan plan = await _rentalPlanRepository.GetByIdAsync(contract.RentanPlanId);

            if (plan == null)
                throw new NotFoundException("Rental Plan not found!");

            contract.FinalizeRental(plan, endDate);

            return new CheckTotalRentalPriceDto(contractId,
                                                plan.Descrition,
                                                plan.DayValue,
                                                contract.RentalValue,
                                                contract.AdditionalFineValue,
                                                contract.AdditionalDailyValue,
                                                contract.TotalRentalValue
                                                );
        }

        public async Task<SimulationDto> SimulateValuesContract(DateTime startData, DateTime endDate)
        {
            SimulationDto simulation = new SimulationDto(startData, endDate);
            RentalContract contract;
            var plans = await GetPossiblePlans(simulation.StartDate, simulation.EndDate);
            int daysUsed = (simulation.EndDate - simulation.StartDate).Days;

            PlanDto planDto;
            foreach (var plan in plans)
            {
                contract = new RentalContract(Guid.NewGuid(), plan.Id, simulation.StartDate, simulation.StartDate.AddDays(plan.Days));
                int difference = Math.Abs(plan.Days - daysUsed);

                //contract.SetPlan(plan);
                contract.SetRentalValue(plan.Days * plan.DayValue);
                contract.FinalizeRental(plan, simulation.EndDate);
                planDto = new PlanDto(plan.Descrition, plan.DayValue,
                                      contract.RentalValue, contract.AdditionalFineValue, contract.AdditionalDailyValue,
                                      contract.TotalRentalValue);

                planDto.Observation.Add($"Once the motorcycle is delivered on the scheduled date, the amount to be paid will be: {planDto.RentalValue}");
                planDto.Observation.Add($"Total days of the contracted plan: {plan.Days}");

                if (contract.AdditionalFineValue > 0)
                {
                    planDto.Observation.Add($"Total days less than the contracted plan: {difference}");
                    if (plan.Days is 7 or 15)
                    {
                        planDto.Observation.Add($"The value of {contract.AdditionalFineValue} refers to a {plan.PercentageFine}% fine for breach of contract, upon delivery {difference} days before the end date of the rental.");
                        planDto.Observation.Add("");
                        planDto.Observation.Add($"Obs - Calc: ( {difference} days x {plan.DayValue} ) + {plan.PercentageFine}% = {((difference * plan.DayValue) * (plan.PercentageFine / 100))}");
                    }
                }

                if (contract.AdditionalDailyValue > 0)
                {
                    planDto.Observation.Add($"Total additional days to the contracted plan: {difference}");
                    planDto.Observation.Add($"The value of {contract.AdditionalDailyValue} refers to the additional value of R$50.00 per additional day.");
                    planDto.Observation.Add($"");
                    planDto.Observation.Add($"Obs - Calc: {difference} dias x {plan.AdditionalValueDaily} additional value = {difference * plan.AdditionalValueDaily}.");
                }
                simulation.Plans.Add(planDto);
            }
            return simulation;
        }

        private async Task<List<RentalPlan>> GetPossiblePlans(DateTime StartData, DateTime EndDate)
        {
            int difference = (EndDate - StartData).Days;
            var plans = await _rentalPlanRepository.GetAllAsync();

            List<RentalPlan> possiblePlans = plans.Where(p => p.Days <= difference).ToList();

            RentalPlan lastplan = plans.Where(p => p.Days > difference).OrderBy(p => p.Days).FirstOrDefault();

            if (lastplan != null)
            {
                possiblePlans.Add(lastplan);
            }

            return possiblePlans;
        }


    }
}

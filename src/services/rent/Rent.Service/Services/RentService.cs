using AutoMapper;
using BusConnections.Core.Model;
using BusConnections.Events.Producer;
using Rent.Core.Exceptions;
using Rent.Domain.Entities;
using Rent.Infra.Interfaces;
using Rent.Service.DTO;
using Rent.Service.Interfaces;

namespace Rent.Service.Services;

public class RentService : IRentService
{
    private readonly IMapper _mapper;
    private readonly ITransactionRepository _rentRepository;
    private readonly MbClient _client;
    public RentService(IMapper mapper, ITransactionRepository planRepository)
    {
        _mapper = mapper;
        _rentRepository = planRepository;
    }
    async Task<TransactionDTO> IRentService.Create(TransactionDTO transactionDto)
    {
        var manager = GetUser(transactionDto.Manager);
        var deliveryMan = GetUser(transactionDto.DeliveryMan);
        var cnhType = deliveryMan!.Payload.CnhType;
        if(cnhType != "A") throw new DomainException("It's mandatory 'A' CNH type.");
        if (manager!.Payload.Role.ToString() != "admin") throw new DomainException("The user must have administrator access.");
        var motorcycle = GetMotorcycle(transactionDto.Motorcycle);
        if (motorcycle.Payload.Status == "Rented") throw new DomainException("This plan is already registered");
        var transaction = _mapper.Map<Transaction>(transactionDto);
        transactionDto.StartDate = DateTime.Now.AddDays(1);
        var plan =  GetPlan(transactionDto.Plan);
        transactionDto.EndDate = DateTime.Now.AddDays(1 + plan!.Payload.Days);
        transaction.Validate();
        var newTransaction = await _rentRepository.Create(transaction);
        return _mapper.Map<TransactionDTO>(newTransaction);
    }

    async Task<TransactionDTO> IRentService.Get(long id)
    {
        var rentDto = await _rentRepository.Get(id);
        var plan =  GetPlan(rentDto.Plan);
        var rent = _mapper.Map<TransactionDTO>(rentDto);
        rent.Price = ComputeLateFee(plan, rentDto.StartDate);
        return rent;
    }

    internal dynamic GetUser(long userId)
    {
        
        var user = _client.Call(new Request
        {
            Method = "GetUser",
            Payload = new { Id = userId }
        });
        return user!;
    }
    internal dynamic GetPlan(long planId)
    {
        
        var plan = _client.Call(new Request
        {
            Method = "GetPlan",
            Payload = new { Id = planId }
        });
        return plan!;
    }
    internal dynamic GetMotorcycle(long motorcycleId)
    {
        
        var motorcycle = _client.Call(new Request
        {
            Method = "GetMotorcycle",
            Payload = new { Id = motorcycleId }
        });
        return motorcycle!;
    }
    internal dynamic SetMotorCycleStatus(long motorcycleId, string status)
    {
        
        var motorcycle = _client.Call(new Request
        {
            Method = "SetMotorcycleStatus",
            Payload = new { Id = motorcycleId, Status = status }
        });
        return motorcycle!;
    }
    internal decimal ComputeLateFee(dynamic plan, DateTime startDate)
    {
        var days = plan!.Payload.Days;
        var endDate = startDate.AddDays(days);
        if (endDate < DateTime.Now) return 0;
        var exceedDays = ((int)(DateTime.Now - endDate).TotalDays).ToString();
        return decimal.Parse(plan!.Payload.LateFee)*exceedDays;
    }
    async Task<List<TransactionDTO>> IRentService.GetAll()
    {
        var transactionDto = await _rentRepository.GetAll();
        return _mapper.Map<List<TransactionDTO>>(transactionDto);
    }

    async Task IRentService.Remove(long id)
    {
        await _rentRepository.Delete(id);
    }

    async Task<TransactionDTO> IRentService.Update(TransactionDTO transactionDTO)
    {
        var hasRent = await _rentRepository.Get(transactionDTO.Id);
        if (hasRent == null) throw new DomainException("Rent not found");
        var transaction = _mapper.Map<Transaction>(transactionDTO);
        transaction.Validate();
        var newTransaction = await _rentRepository.Create(transaction);
        return _mapper.Map<TransactionDTO>(newTransaction);
    }

    async Task<TransactionDTO> IRentService.SetEndDate(long id, string endDate, long deliveryManId)
    {
       var deliveryMan = GetUser(deliveryManId);
        var hasRent = await _rentRepository.Get(id);
        if (hasRent == null) throw new DomainException("Rent not found");
        if (hasRent.DeliveryMan != deliveryMan!.Payload.CnhType) throw new DomainException("You're not able to update this rent end date");
        var transactionDto = _mapper.Map<TransactionDTO>(hasRent);
        transactionDto.EndDate = DateTime.Parse(endDate);
        var plan =  GetPlan(transactionDto.Plan);
        transactionDto.Price = ComputeLateFee(plan, DateTime.Parse(endDate)) + plan!.Payload.Days*plan!.Payload.DailyRate;
        var transaction = _mapper.Map<Transaction>(transactionDto);
        transaction.Validate();
        var newTransaction = await _rentRepository.Create(transaction);
        return _mapper.Map<TransactionDTO>(newTransaction);
    }
}
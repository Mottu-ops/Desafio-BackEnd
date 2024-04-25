using AutoMapper;
using BusConnections.Core.Model;
using BusConnections.Events.Producer;
using Plan.Core.Execeptions;
using Plan.Domain.Entity;
using Plan.Infra.interfaces;
using Plan.Service.DTO;
using Plan.Service.Interfaces;

namespace Plan.Service.Services;

public class PlanService : IPlanServices
{
    private readonly IMapper _mapper;
    private readonly IPlanRepository _planRepository;
    private readonly MbClient _mBClient;
    public PlanService(IMapper mapper, IPlanRepository planRepository, MbClient mBClient)
    {
        _mapper = mapper;
        _planRepository = planRepository;
        _mBClient = mBClient;
    }

    async Task<PlanDto> IPlanServices.Create(PlanDto planDto)
    {
        CheckUserRole(planDto.User);
        var _hasPlan = await _planRepository.Get(planDto.Id);
        if (_hasPlan != null) throw new DomainException("This plan is already registered");
        var plan = _mapper.Map<RentPlan>(planDto);
        plan.Validate();
        var newPlan = await _planRepository.Create(plan);
        return _mapper.Map<PlanDto>(newPlan);
    }

    internal void CheckUserRole(long userId)
    {
        
        var user = _mBClient.Call(new Request
        {
            Method = "GetUser",
            Payload = new { Id = userId }
        });
        var userRole = user!.Payload.Role.ToString();
        if (userRole != "admin") throw new DomainException("The user must have administrator access.");
    }
    async Task<PlanDto> IPlanServices.Get(long id, long userId)
    {
        CheckUserRole(userId);
        var PlanDto = await _planRepository.Get(id);
        return _mapper.Map<PlanDto>(PlanDto);
    }

    async Task<List<PlanDto>> IPlanServices.GetAll(long userId)
    {
        CheckUserRole(userId);
        var planDto = await _planRepository.GetAll();
        return _mapper.Map<List<PlanDto>>(planDto);
    }

    async Task IPlanServices.Remove(long id, long userId)
    {
        CheckUserRole(userId);
        await _planRepository.Delete(id);
    }

    async Task<PlanDto> IPlanServices.Update(PlanDto planDto)
    {
        CheckUserRole(planDto.User);
        var _hasPlan = await _planRepository.Get(planDto.Id);
        if (_hasPlan == null) throw new DomainException("Plan not found");
        var plan = _mapper.Map<RentPlan>(planDto);
        plan.Validate();
        var newPlan = await _planRepository.Update(plan);
        return _mapper.Map<PlanDto>(newPlan);
    }
}

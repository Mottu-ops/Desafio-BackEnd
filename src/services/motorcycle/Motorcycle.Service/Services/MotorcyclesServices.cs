using AutoMapper;
using BusConnections.Core.Model;
using BusConnections.Events.Producer;
using Motorcycle.Core.Exceptions;
using Motorcycle.Domain.Entities;
using Motorcycle.Infra.Interfaces;
using Motorcycle.Service.DTO;
using Motorcycle.Service.Interfaces;

namespace Motorcycle.Service.Services;

public class MotorcycleService : IMotorcycleServices
{
    private readonly IMapper _mapper;
    private readonly IMotorCycleRepository _motorcycleRepository;
    private readonly MbClient _mBClient;
    public MotorcycleService(IMapper mapper, IMotorCycleRepository motorcycleRepository, MbClient mBClient)
    {
        _mapper = mapper;
        _motorcycleRepository = motorcycleRepository;
        _mBClient = mBClient;
    }

    async Task<VehicleDto> IMotorcycleServices.Create(VehicleDto vehicleDTO)
    {
        CheckUserRole(vehicleDTO.Owner);
        var _hasVehicle = await _motorcycleRepository.Get(vehicleDTO.PlateCode);
        if (_hasVehicle != null) throw new DomainException("This vehicle is already registered");
        var vehicle = _mapper.Map<Vehicle>(vehicleDTO);
        vehicle.Validate();
        var newVehicle = await _motorcycleRepository.Create(vehicle);
        return _mapper.Map<VehicleDto>(newVehicle);
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

    async Task<VehicleDto> IMotorcycleServices.Get(long id, long userId)
    {
        CheckUserRole(userId);
        var vehicleDTO = await _motorcycleRepository.Get(id);
        return _mapper.Map<VehicleDto>(vehicleDTO);
    }
    async Task<VehicleDto> IMotorcycleServices.Get(string plateCode, long userId)
    {
        CheckUserRole(userId);
        var vehicleDTO = await _motorcycleRepository.Get(plateCode);
        return _mapper.Map<VehicleDto>(vehicleDTO);
    }

    async Task<List<VehicleDto>> IMotorcycleServices.GetAll(long userId)
    {
        CheckUserRole(userId);
        var vehiclesDTO = await _motorcycleRepository.GetAll();
        return _mapper.Map<List<VehicleDto>>(vehiclesDTO);
    }

    async Task IMotorcycleServices.Remove(long id, long userId)
    {
        CheckUserRole(userId);
        await _motorcycleRepository.Delete(id);
    }

    async Task<VehicleDto> IMotorcycleServices.Update(VehicleDto vehicleDTO)
    {
        CheckUserRole(vehicleDTO.Owner);
        var _hasVehicle = await _motorcycleRepository.Get(vehicleDTO.Id);
        if (_hasVehicle == null) throw new DomainException("Vehicle not found");
        var vehicle = _mapper.Map<Vehicle>(vehicleDTO);
        vehicle.Validate();
        var newVehicle = await _motorcycleRepository.Update(vehicle);
        return _mapper.Map<VehicleDto>(newVehicle);
    }
}

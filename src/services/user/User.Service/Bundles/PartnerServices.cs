using AutoMapper;
using User.Core.Execeptions;
using User.Domain.Entities;
using User.Infra.interfaces;
using User.Service.DTO;
using User.Service.Interfaces;

namespace User.Service.Bundles;

public class PartnerServices : IPartnerServices
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;

    public PartnerServices(IMapper mapper, IUserRepository userRepository){
        _mapper = mapper;
        _userRepository = userRepository;
    }

    async Task<PartnerDto> IPartnerServices.Create(PartnerDto partnerDTO)
    {
        var hasUser = await _userRepository.GetByEmail(partnerDTO.Email);
        if(hasUser != null) throw new DomainException("This e-mail is invalid");
        var user = _mapper.Map<Partner>(partnerDTO);
        user.Validate();
        var newUser =  await _userRepository.Create(user);
        return _mapper.Map<PartnerDto>(newUser);

    }

    async Task<PartnerDto> IPartnerServices.Get(long id)
    {
        var partnerDTO = await _userRepository.Get(id);
        return _mapper.Map<PartnerDto>(partnerDTO);
    }
    async Task<PartnerDto> IPartnerServices.Get(string email)
    {
        var partnerDTO = await _userRepository.GetByEmail(email);
        return _mapper.Map<PartnerDto>(partnerDTO);
    }

    async Task<List<PartnerDto>> IPartnerServices.GetAll()
    {
        var partners = await _userRepository.GetAll();
        return _mapper.Map<List<PartnerDto>>(partners);
    }

    async Task IPartnerServices.Remove(long id)
    {
        await _userRepository.Delete(id);
    }

    async Task<PartnerDto> IPartnerServices.Update(PartnerDto partnerDTO)
    {
        var hasUser = await _userRepository.Get(partnerDTO.Id);
        if(hasUser == null) throw new DomainException("This e-mail is invalid");
        var user = _mapper.Map<Partner>(partnerDTO);
        user.Validate();
        var newUser =  await _userRepository.Update(user);
        return _mapper.Map<PartnerDto>(newUser);

    }
}
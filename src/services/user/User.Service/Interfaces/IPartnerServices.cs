using User.Service.DTO;

namespace User.Service.Interfaces;

public interface IPartnerServices{
    Task<PartnerDto> Create(PartnerDto partnerDto);
    Task<PartnerDto> Update(PartnerDto partnerDto);
    Task Remove(long id);
    Task<PartnerDto> Get(long id);
    Task<PartnerDto> Get(string email);
    Task<List<PartnerDto>> GetAll();
} 
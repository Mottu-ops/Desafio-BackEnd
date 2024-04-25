using User.Domain.Entities;

namespace User.Infra.interfaces
{
    public interface IUserRepository : IBaseRepository<Partner>{
        Task<Partner> GetByEmail(string email);
        Task<List<Partner>> GetByCnpj(string cnpj);
        Task<List<Partner>> GetAll();
        Task<Partner> GetByCnh(string cnh);
    }
}
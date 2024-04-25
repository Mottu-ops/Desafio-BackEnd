using Microsoft.EntityFrameworkCore;
using User.Domain.Entities;
using User.Infra.context;
using User.Infra.interfaces;

namespace User.Infra.Repositories{
    public class UserRepository : BaseRepository<Partner>, IUserRepository
{
    private readonly UserContext _context;
    public UserRepository(UserContext context) : base(context)
    {
        _context = context;
    }

        Task<List<Partner>> IUserRepository.GetAll()
    {
            var users = _context.Partners.AsNoTracking().ToListAsync();
            return users;

        }

    Task<Partner> IUserRepository.GetByCnh(string cnh)
    {
        throw new NotImplementedException();
    }

    Task<List<Partner>> IUserRepository.GetByCnpj(string cnpj)
    {
        throw new NotImplementedException();
    }

    async Task<Partner> IUserRepository.GetByEmail(string email)
    {
        var user = await _context.Partners.Where(x => x.Email.ToLower() == email.ToLower()).AsNoTracking().ToListAsync();
        return user.FirstOrDefault()!;
    }
}
}


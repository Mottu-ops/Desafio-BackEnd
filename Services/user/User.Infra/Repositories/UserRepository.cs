using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Domain.Entities;
using User.Infra.Context;
using User.Infra.Interfaces;

namespace User.Infra.Repositories
{
    public class UserRepository : ModelBaseRepository<Client>, IUserRepository
    {
        private readonly UserContext _context;
        public UserRepository(UserContext context) : base(context)
        {
            _context = context;
        }

        public Task<List<Client>> GetAll()
        {
            var users = _context.Clients.AsNoTracking().ToListAsync();
            return users;
        }

        public async Task<Client> GetByCnh(string cnh)
        {
            var user = await _context.Clients.Where(x => x.CnhNumber.ToLower() == cnh).AsNoTracking().ToListAsync();
            return user.FirstOrDefault()!;
        }

        public async Task<Client> GetByCPFCNPJ(string cpfcpnj)
        {
            var user = await _context.Clients.Where(x => x.CPFCnpj.ToLower() == cpfcpnj).AsNoTracking().ToListAsync();
            return user.FirstOrDefault()!;
        }

        public async Task<Client> GetByEmail(string email)
        {
            var user = await _context.Clients.Where(x => x.Email.ToLower() == email.ToLower()).AsNoTracking().ToListAsync();
            return user.FirstOrDefault()!;
        }
       
    }
}

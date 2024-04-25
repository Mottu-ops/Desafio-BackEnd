using Microsoft.EntityFrameworkCore;
using Rent.Domain.Entities;
using Rent.Infra.Context;
using Rent.Infra.Interfaces;

namespace Rent.Infra.Repositories;

public class TransactionRepository : BaseRepository<Transaction>, ITransactionRepository
{
    private readonly TransactionContext _context;
    public TransactionRepository(TransactionContext context) : base(context)
    {
        _context = context;
    }

    Task<List<Transaction>> ITransactionRepository.GetAll()
    {
        var transactions = _context.Motorcycles.AsNoTracking().ToListAsync();
        return transactions;
    }
}
using Rent.Domain.Entities;

namespace Rent.Infra.Interfaces;
public interface ITransactionRepository : IBaseRepository<Transaction> {
    Task<List<Transaction>> GetAll();
}
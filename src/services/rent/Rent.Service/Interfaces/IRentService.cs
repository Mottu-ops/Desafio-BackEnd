using Rent.Service.DTO;

namespace Rent.Service.Interfaces;
public interface IRentService {
    Task<TransactionDTO> Create(TransactionDTO transactionDTO);
    Task<TransactionDTO> Update(TransactionDTO transactionDTO);
    Task Remove(long id);
    Task<TransactionDTO> Get(long id);
    Task<TransactionDTO> SetEndDate(long id, string endDate, long deliveryManId);
    Task<List<TransactionDTO>> GetAll();
}
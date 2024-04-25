using System.Transactions;
using AutoMapper;
using Rent.Service.DTO;

namespace Rent.Service.Profiles;
public class TransactionProfile : Profile {
    public TransactionProfile () {
        CreateMap<TransactionDTO, Transaction>();
    }
}
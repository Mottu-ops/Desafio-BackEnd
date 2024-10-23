using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MT.Backend.Challenge.Application.Queries.Rentals.GetRentalById
{
    public class GetRentalByIdRequest : IRequest<GetRentalByIdResponse>
    {
        public string Id { get; set; } = null!;
    }
}

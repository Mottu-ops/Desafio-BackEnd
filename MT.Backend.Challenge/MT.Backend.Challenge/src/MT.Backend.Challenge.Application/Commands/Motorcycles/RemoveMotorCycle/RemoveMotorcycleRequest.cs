using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MT.Backend.Challenge.Application.Commands.Motorcycles.RemoveMotorcycle
{
    public class RemoveMotorcycleRequest: IRequest<RemoveMotorcycleResponse>
    {
        public string Id { get; set; }
    }
}

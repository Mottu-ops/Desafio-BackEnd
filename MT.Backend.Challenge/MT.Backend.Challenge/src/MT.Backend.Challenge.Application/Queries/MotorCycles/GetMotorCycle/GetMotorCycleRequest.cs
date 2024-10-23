using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MT.Backend.Challenge.Application.Queries.Motorcycles.GetMotorCycle
{
    public class GetMotorCycleRequest : IRequest<GetMotorCycleResponse>
    {
        [JsonPropertyName("placa")]
        public string? LicensePlate { get; set; }
    }
}

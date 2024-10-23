using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MT.Backend.Challenge.Application.Commands.Rentals.ChangeRental
{
    public class ChangeRentalRequest : IRequest<ChangeRentalResponse>
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = null!;
        [JsonPropertyName("data_devolucao")]
        public DateTime EstimatedEndDate { get; set; }
    }
}

using MediatR;
using MT.Backend.Challenge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MT.Backend.Challenge.Application.Commands.Rentals.AddRental
{
    public class AddRentalRequest : IRequest<AddRentalResponse>
    {
        [JsonPropertyName("entregador_id")]
        public string DeliveryDriverId { get; set; } = null!;
        [JsonPropertyName("moto_id")]
        public string MotorcycleId { get; set; } = null!;
        [JsonPropertyName("data_inicio")]
        public DateTime StartDate { get; set; }
        [JsonPropertyName("data_termino")]
        public DateTime EndDate { get; set; }
        [JsonPropertyName("data_previsao_termino")]
        public DateTime EstimatedEndDate { get; set; }
        [JsonPropertyName("plano")]
        public int RentalCategoryDays { get; set; }

    }
}

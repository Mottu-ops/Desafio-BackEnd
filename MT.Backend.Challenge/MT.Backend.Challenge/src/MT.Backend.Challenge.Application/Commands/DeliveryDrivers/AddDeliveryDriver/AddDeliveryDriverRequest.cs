using MediatR;
using MT.Backend.Challenge.Domain.Entities;
using System;
using System.Text.Json.Serialization;

namespace MT.Backend.Challenge.Application.Commands.DeliveryDrivers.AddDeliveryDriver
{
    public class AddDeliveryDriverRequest : IRequest<AddDeliveryDriverResponse>
    {
        [JsonPropertyName("identificador")]
        public string Id { get; set; } = null!;
        [JsonPropertyName("nome")]
        public string Name { get; set; } = null!;
        [JsonPropertyName("cnpj")]
        public string Document { get; set; } = null!;
        [JsonPropertyName("data_nascimento")]
        public DateTime BirthDate { get; set; }
        [JsonPropertyName("numero_cnh")]
        public string DriversLicenseNumber { get; set; } = null!;
        [JsonPropertyName("tipo_cnh")]
        public DriversLicenseCategory DriversLicenseCategory { get; set; }
        [JsonPropertyName("validade_cnh")]
        public DateTime? DriversLicenseValidDate { get; set; }
        [JsonPropertyName("imagem_cnh")]
        public string DriversLicenseImage { get; set; } = null!;

    }
}

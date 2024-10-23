using MediatR;
using MT.Backend.Challenge.Domain.Entities;
using System;
using System.Text.Json.Serialization;

namespace MT.Backend.Challenge.Application.Commands.DeliveryDrivers.AddDeliveryDriverLicense
{
    public class AddDeliveryDriverLicenseRequest : IRequest<AddDeliveryDriverLicenseResponse>
    {
        public string Id { get; set; } = null!;
        
        [JsonPropertyName("imagem_cnh")]
        public string DriversLicenseImage { get; set; } = null!;

    }
}

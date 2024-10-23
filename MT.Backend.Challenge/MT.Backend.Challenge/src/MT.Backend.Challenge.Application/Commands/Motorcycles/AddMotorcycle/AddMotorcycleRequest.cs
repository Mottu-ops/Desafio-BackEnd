using MediatR;
using MT.Backend.Challenge.Domain.Entities;
using System;
using System.Text.Json.Serialization;

namespace MT.Backend.Challenge.Application.Commands.Motorcycles.AddMotorcycle
{
    public class AddMotorcycleRequest : IRequest<AddMotorcycleResponse>
    {
        [JsonPropertyName("identificador")]
        public string Id { get; set; } = null!;
        [JsonPropertyName("ano")]
        public int Year { get; set; }
        [JsonPropertyName("modelo")]
        public string Model { get; set; } = null!;
        [JsonPropertyName("placa")]
        public string LicensePlate { get; set; } = null!;


    }
}

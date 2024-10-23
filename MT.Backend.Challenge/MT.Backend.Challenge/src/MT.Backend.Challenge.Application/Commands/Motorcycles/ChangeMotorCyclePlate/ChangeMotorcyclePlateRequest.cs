using MediatR;
using System.Text.Json.Serialization;

namespace MT.Backend.Challenge.Application.Commands.Motorcycles.ChangeMotorcyclePlate
{
    public class ChangeMotorcyclePlateRequest: IRequest<ChangeMotorcyclePlateResponse>
    {
        public string Id { get; set; }
        [JsonPropertyName("placa")]
        public string NewPlateNumber { get; set; } = null!;
    }
}

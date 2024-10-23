using MediatR;

namespace MT.Backend.Challenge.Application.Queries.Motorcycles.GetMotorCycleById
{
    public class GetMotorCycleByIdRequest : IRequest<GetMotorCycleByIdResponse>
    {
        public string Id { get; set; } = null!;
    }
}

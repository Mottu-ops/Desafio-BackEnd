using Job.Domain.Enums;

namespace Job.Domain.Entities.Rent;

public class RentEntity : BaseEntity
{
    public RentEntity(Guid idMotoboy, Guid idMoto, DateOnly datePreview, EPlan plan)
    {
        DateStart = DateOnly.FromDateTime(DateTime.Now.AddDays(1));
        IdMotoboy = idMotoboy;
        IdMoto = idMoto;
        DatePreview = datePreview;
        Plan = plan;
        DateEnd = DateStart.AddDays((int) plan);
        CalculateRent();
    }

    public DateOnly DateStart { get; private set; }
    public DateOnly DateEnd { get; private set; }
    public DateOnly DatePreview { get; private set; }
    public Guid IdMoto { get; private set; }
    public Guid IdMotoboy { get; private set; }
    public EPlan Plan { get; private set; }
    public decimal Value { get; private set; }
    public decimal? Fine { get; private set; }

    private void CalculateRent()
    {
        Value = Plan switch
        {
            EPlan.Sete => 7 * 30,
            EPlan.Quinze => 15 * 28,
            EPlan.Trinta => 30 * 22,
            EPlan.QuarentaCinco => 45 * 20,
            EPlan.Cinquenta => 50 * 18,
            _ => 0
        };
    }

    public decimal CalculateFine(DateOnly datePreview)
    {
        base.Update();
        DatePreview = datePreview;
        var days = DateEnd.DayNumber - DatePreview.DayNumber;

        if (DateEnd < DatePreview)
        {
            Fine = 50 * (days * -1);
            return Fine ?? 0;
        }

        if (days <= 0) return 0;

        Fine = Plan switch
        {
            EPlan.Sete => ((Value / (int) Plan) * 0.2m) * days,
            _ => ((Value / (int) Plan) * 0.4m) * days,
        };

        return Fine ?? 0;
    }
}
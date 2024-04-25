namespace Plan.Service.DTO;
public class PlanDto
{
    public long Id { get; set; }
    public string Name {get; set;}
    public decimal DailyRate {get; set;}
    public int Days {get; set;}
    public long User {get; set;}

    public decimal LateFee  {get; set;}

    public PlanDto() { }

    public PlanDto(long id, string name, decimal dailyRate, int days, long user, decimal lateFee)
    {
        Id = id;
        Name = name;
        DailyRate = dailyRate;
        Days = days;
        User = user;
        LateFee = lateFee;
    }
}
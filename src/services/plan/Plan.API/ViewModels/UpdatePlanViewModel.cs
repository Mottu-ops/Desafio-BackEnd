namespace Plan.API.ViewModels;
public class UpdatePlanViewModel
{
    public long Id {get; set;}
    public string Name {get; set;}
    public decimal DailyRate {get; set;}
    public int Days {get; set;}
    public long User {get; set;}
    public decimal LateFee {get; set;}

}
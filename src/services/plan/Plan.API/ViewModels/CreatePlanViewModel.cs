namespace Plan.API.ViewModels;
public class CreatePlanViewModel
{
    public string Name {get; set;}
    public decimal DailyRate {get; set;}
    public int Days {get; set;}
    public long User {get; set;}
    public decimal LateFee {get; set;}
}
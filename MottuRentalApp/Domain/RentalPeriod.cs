namespace MottuRentalApp.Domain
{
  public class RentalPeriod
  {
    public RentalPeriod(string identifier, int daysNumber, decimal dailyPrice)
    {
      Identifier = identifier;
      DaysNumber = daysNumber;
      DailyPrice = dailyPrice;
    }

    public string Identifier { get; }
    public int DaysNumber { get; }
    public decimal DailyPrice { get; }
    public decimal PeriodPrice { get => DaysNumber * DailyPrice; }
  }
}
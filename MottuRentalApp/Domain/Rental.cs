namespace MottuRentalApp.Domain
{
  public class Rental
  {
    public Rental(string userId, string vehicleId, string endTerm)
    {
      Identifier = Guid.NewGuid().ToString();
      UserId = userId;
      VehicleId = vehicleId;
      StartTerm = DateTime.UtcNow.AddDays(1.00);
      EndTerm = DateTime.Parse(endTerm);
    }

    public string Identifier { get; }
    public string UserId { get; }
    public string VehicleId { get; set; }
    public DateTime StartTerm { get; set; }
    public DateTime EndTerm { get; set; }
    public RentalStatus Status { get => GetCurrentStatus(); }   
    public decimal TotalFare { get; set; }

    private RentalStatus GetCurrentStatus() {
      if (DateTime.UtcNow.CompareTo(StartTerm) < 0) {
        return RentalStatus.PENDING;
      } else if (DateTime.UtcNow.CompareTo(StartTerm) >= 0 && DateTime.UtcNow.CompareTo(EndTerm) < 0) {
        return RentalStatus.ACTIVE;
      } else {
        return RentalStatus.DONE;
      }
    }
  }
}

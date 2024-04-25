namespace Rent.Service.DTO;
public class TransactionDTO {
    public long Id { get; set; }
    public long DeliveryMan { get; set; }
    public long Manager { get; set; }
    public long Motorcycle { get; set; }
    public long Plan { get; set; }
    public DateTime StartDate{get; set;}
    public DateTime EndDate{get; set;}
    public decimal Price {get; set;}
    protected TransactionDTO() {}

    public TransactionDTO(long id, long deliveryMan, long manager, long motorcycle, long plan, DateTime startDate, DateTime endDate, decimal price)
    {
        Id = id;
        DeliveryMan = deliveryMan;
        Manager = manager;
        Motorcycle = motorcycle;
        Plan = plan;
        StartDate =  startDate;
        EndDate = endDate;
        Price = price;
    }
}

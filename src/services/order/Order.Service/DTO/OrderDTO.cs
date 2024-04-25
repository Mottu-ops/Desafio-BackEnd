namespace Order.Service.DTO;

public class OrderDTO
{
    public long Id { get; set; }
    public decimal Price { get; private set; }
    public string Situation { get; private set; }

    protected OrderDTO() { }

    public OrderDTO(long id, decimal price, string situation)
    {
        Id = id;
        Price = price;
        Situation = situation;
    }
}


namespace Order.Domain.Entities
{
    public class OrderEntity : Base
    {

        public decimal Price { get; private set; }
        public string Situation { get; private set; }


        protected OrderEntity() { }

        public OrderEntity(decimal price, string situation)
        {
            Price = price;
            Situation = situation;
        }

        public override bool Validate()
        {
            throw new NotImplementedException();
        }
    }

}

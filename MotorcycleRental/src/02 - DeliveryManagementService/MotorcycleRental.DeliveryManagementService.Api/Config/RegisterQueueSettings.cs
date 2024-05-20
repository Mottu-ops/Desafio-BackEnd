namespace MotorcycleRental.DeliveryManagementService.Api.Config
{
    public class RegisterQueueSettings
    {
        public string Exchange { get; set; }
        public string Queue { get; set; }
        public string RoutingKey { get; set; }
        public string TypeExchange { get; set; }
        public ushort PrefetchCount { get; set; }
    }
}

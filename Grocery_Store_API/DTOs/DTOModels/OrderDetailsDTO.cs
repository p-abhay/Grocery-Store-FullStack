namespace DataAccessLayer.Repository
{
    public class OrderDetailsDTO //this is for sending my orders to controller
    {
        public Guid OrderId { get; set; }
        public string ProductName { get; internal set; }
        public string ProductImage { get; internal set; }
        public int Quantity { get; internal set; }
        public string Date { get; internal set; }

    }
}
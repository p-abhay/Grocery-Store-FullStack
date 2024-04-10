namespace DataAccessLayer.Repository
{
    public class OrderDetails // this is for sending myorders to the service
    {
        public Guid OrderId { get; set; }
        public string ProductName { get; internal set; }
        public string ProductImage { get; internal set; }
        public int Quantity { get; internal set; }
        public string Date { get; internal set; }

    }
}
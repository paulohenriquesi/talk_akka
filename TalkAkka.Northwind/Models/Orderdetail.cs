namespace TalkAkka.Northwind
{
    public class Orderdetail
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public float UnitPrice { get; set; }
        public int Quantity { get; set; }
        public float Discount { get; set; }
    }

}

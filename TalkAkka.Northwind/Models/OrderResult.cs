namespace TalkAkka.Northwind
{
    public class OrderResult
    {
        public Order Order { get; set; }
        public Orderdetail[] OrderDetails { get; set; }
    }
}
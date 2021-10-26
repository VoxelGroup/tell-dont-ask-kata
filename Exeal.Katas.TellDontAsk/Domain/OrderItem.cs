namespace Exeal.Katas.TellDontAsk.Domain
{
    public class OrderItem
    {
        public OrderItem(Product product, int quantity, decimal taxedAmount, decimal tax)
        {
            Product = product;
            Quantity = quantity;
            TaxedAmount = taxedAmount;
            Tax = tax;
        }

        public Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal TaxedAmount { get; set; }
        public decimal Tax { get; set; }
    }
}
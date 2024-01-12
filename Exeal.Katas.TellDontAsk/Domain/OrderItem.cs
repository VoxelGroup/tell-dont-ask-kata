namespace Exeal.Katas.TellDontAsk.Domain
{
    public class OrderItem
    {
        public OrderItem(Product product, int quantity, decimal tax, decimal taxedAmount)
        {
            Product = product;
            Quantity = quantity;
            Tax = tax;
            TaxedAmount = taxedAmount;
        }

        public Product Product { get; }
        public int Quantity { get; }
        public decimal TaxedAmount { get; }
        public decimal Tax { get; }
    }
}
namespace Exeal.Katas.TellDontAsk.Domain
{
    public class OrderItem
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal TaxedAmount { get; set; }
        public decimal Tax { get; set; }

        public OrderItem(Product product, int quantity, decimal taxAmount, decimal taxedAmount)
        {
            Product = product;
            Quantity = quantity;
            Tax = taxAmount;
            TaxedAmount = taxedAmount;
        }
    }
}
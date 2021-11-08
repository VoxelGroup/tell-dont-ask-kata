namespace Exeal.Katas.TellDontAsk.Domain
{
    public class OrderItem
    {
        public OrderItem(Product product, int quantity)
        {
            Product = product;
            Quantity = quantity;
            TaxedAmount = product.TaxedAmount(quantity);
            Tax = product.TaxAmount(quantity);
        }

        public Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal TaxedAmount { get; set; }
        public decimal Tax { get; set; }
    }
}
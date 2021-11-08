using System;

namespace Exeal.Katas.TellDontAsk.Domain
{
    public class OrderItem
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal TaxedAmount { get; set; }
        public decimal Tax { get; set; }

        public OrderItem(Product product,int quantity)
        {
            decimal unitaryTax = Math.Round(product.Price / 100M * product.Category.TaxPercentage, 2, MidpointRounding.AwayFromZero);
            decimal unitaryTaxedAmount = Math.Round(product.Price + unitaryTax, 2, MidpointRounding.AwayFromZero);
            decimal taxedAmount = Math.Round(unitaryTaxedAmount * quantity, 2, MidpointRounding.AwayFromZero);
            decimal taxAmount = Math.Round(unitaryTax * quantity, 2, MidpointRounding.AwayFromZero);

            Product = product;
            Quantity = quantity;
            Tax = taxAmount;
            TaxedAmount = taxedAmount;
        }
    }
}
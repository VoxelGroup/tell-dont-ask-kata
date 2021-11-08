using System;
using Exeal.Katas.TellDontAsk.Exception;

namespace Exeal.Katas.TellDontAsk.Domain
{
    public class OrderItem
    {
        public Product Product { get; }
        public int Quantity { get; }
        public decimal TaxedAmount { get; }
        public decimal Tax { get; }

        public OrderItem(Product product, int quantity)
        {
            if (product == null)
            {
                throw new UnknownProductException();
            }

            if (quantity <= 0)
            {
                throw new InvalidQuantityException();
            }

            var unitaryTax = Math.Round(product.Price / 100M * product.Category.TaxPercentage, 2,
                MidpointRounding.AwayFromZero);
            var unitaryTaxedAmount = Math.Round(product.Price + unitaryTax, 2, MidpointRounding.AwayFromZero);
            var taxedAmount = Math.Round(unitaryTaxedAmount * quantity, 2, MidpointRounding.AwayFromZero);
            var taxAmount = Math.Round(unitaryTax * quantity, 2, MidpointRounding.AwayFromZero);

            Product = product;
            Quantity = quantity;
            Tax = taxAmount;
            TaxedAmount = taxedAmount;
        }
    }
}
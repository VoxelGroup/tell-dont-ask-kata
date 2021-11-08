using System;
using Exeal.Katas.TellDontAsk.UseCase;

namespace Exeal.Katas.TellDontAsk.Domain
{
    public class Product
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public Category Category { get; set; }

        private decimal UnitaryTax => Math.Round(Price / 100M * Category.TaxPercentage, 2,MidpointRounding.AwayFromZero);

        private decimal UnitaryTaxedAmount => Math.Round(Price + UnitaryTax, 2, MidpointRounding.AwayFromZero);

        public decimal TaxedAmount(int quantity)
        {
            return Math.Round(UnitaryTaxedAmount * quantity, 2, MidpointRounding.AwayFromZero);
        }

        public decimal TaxAmount(int quantity)
        {
            return Math.Round(UnitaryTax * quantity, 2, MidpointRounding.AwayFromZero);
        }
    }
}

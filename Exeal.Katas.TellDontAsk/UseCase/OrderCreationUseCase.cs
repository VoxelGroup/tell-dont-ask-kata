using System;
using System.Collections.Generic;
using Exeal.Katas.TellDontAsk.Domain;
using Exeal.Katas.TellDontAsk.Exception;
using Exeal.Katas.TellDontAsk.Repository;

namespace Exeal.Katas.TellDontAsk.UseCase
{
    public class OrderCreationUseCase
    {
        private readonly OrderRepository orderRepository;
        private readonly ProductCatalog productCatalog;

        public OrderCreationUseCase(OrderRepository orderRepository, ProductCatalog productCatalog)
        {
            this.orderRepository = orderRepository;
            this.productCatalog = productCatalog;
        }

        public void Run(SellItemsRequest request)
        {
            var order = new Order(0M, "EUR", new List<OrderItem>(), 0M, OrderStatus.Created);

            foreach (var itemRequest in request.Requests)
            {
                var product = productCatalog.GetByName(itemRequest.ProductName);

                if (product == null)
                {
                    throw new UnknownProductException();
                }

                var unitaryTax = Math.Round(product.Price / 100M * product.Category.TaxPercentage, 2, MidpointRounding.AwayFromZero);
                var unitaryTaxedAmount = Math.Round(product.Price + unitaryTax, 2, MidpointRounding.AwayFromZero);
                var taxedAmount = Math.Round(unitaryTaxedAmount * itemRequest.Quantity, 2, MidpointRounding.AwayFromZero);
                var taxAmount = Math.Round(unitaryTax * itemRequest.Quantity, 2, MidpointRounding.AwayFromZero);

                var orderItem = new OrderItem(product, itemRequest.Quantity, taxedAmount, taxAmount);
                order.AddItem(orderItem);

                order.CalculateTotal(taxedAmount);
                order.CalculateTax(taxAmount);
            }

            orderRepository.Save(order);
        }
    }
}
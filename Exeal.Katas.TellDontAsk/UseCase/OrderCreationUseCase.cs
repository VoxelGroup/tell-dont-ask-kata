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
            Order order = new Order(OrderStatus.Created, "EUR");

            foreach (SellItemRequest itemRequest in request.Requests)
            {
                Product product = productCatalog.GetByName(itemRequest.ProductName);

                if (product == null)
                {
                    throw new UnknownProductException();
                }

                decimal unitaryTax = Math.Round(product.Price / 100M * product.Category.TaxPercentage, 2, MidpointRounding.AwayFromZero);
                decimal unitaryTaxedAmount = Math.Round(product.Price + unitaryTax, 2, MidpointRounding.AwayFromZero);
                decimal taxedAmount = Math.Round(unitaryTaxedAmount * itemRequest.Quantity, 2, MidpointRounding.AwayFromZero);
                decimal taxAmount = Math.Round(unitaryTax * itemRequest.Quantity, 2, MidpointRounding.AwayFromZero);

                OrderItem orderItem = 
                    new OrderItem(product, itemRequest.Quantity, taxAmount, taxedAmount);

                order.AddItem(orderItem);
            }

            orderRepository.Save(order);
        }
    }
}
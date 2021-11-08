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
            Order order = new Order();
            order.Total = 0M;
            order.Tax = 0M;

            foreach (SellItemRequest itemRequest in request.Requests)
            {
                Product product = productCatalog.GetByName(itemRequest.ProductName);

                if (product == null)
                {
                    throw new UnknownProductException();
                }
                else
                {
                    var taxedAmount = product.TaxedAmount(itemRequest.Quantity);
                    var taxAmount = product.TaxAmount(itemRequest.Quantity);

                    OrderItem orderItem = new OrderItem(product, itemRequest.Quantity);
                    order.AddItem(orderItem);

                    order.Total = order.Total + taxedAmount;
                    order.Tax = order.Tax + taxAmount;
                }
            }

            orderRepository.Save(order);
        }
    }
}
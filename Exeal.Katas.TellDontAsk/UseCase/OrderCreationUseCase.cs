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

            foreach (SellItemRequest itemRequest in request.Requests)
            {
                Product product = productCatalog.GetByName(itemRequest.ProductName);

                if (product == null)
                {
                    throw new UnknownProductException();
                }

                order.AddItem(new OrderItem(product,itemRequest.Quantity));
            }

            orderRepository.Save(order);
        }
    }
}
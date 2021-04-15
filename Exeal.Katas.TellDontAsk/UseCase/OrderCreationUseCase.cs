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
            order.Status = OrderStatus.Created;
            order.Items = new List<OrderItem>();
            order.Currency = "EUR";
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
                    decimal unitaryTax = product.Price / 100M * product.Category.TaxPercentage;
                    decimal unitaryTaxedAmount = product.Price + unitaryTax;
                    decimal taxedAmount = unitaryTaxedAmount * itemRequest.Quantity;
                    decimal taxAmount = unitaryTax * itemRequest.Quantity;

                    OrderItem orderItem = new OrderItem();
                    orderItem.Product = product;
                    orderItem.Quantity = itemRequest.Quantity;
                    orderItem.Tax = taxAmount;
                    orderItem.TaxedAmount = taxedAmount;
                    order.Items.Add(orderItem);

                    order.Total = order.Total + taxedAmount;
                    order.Tax = order.Tax + taxAmount;
                }
            }

            orderRepository.Save(order);
        }
    }
}
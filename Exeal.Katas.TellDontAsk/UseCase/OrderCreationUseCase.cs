using Exeal.Katas.TellDontAsk.Domain;
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
            var order = new Order();
            
            foreach (var itemRequest in request.Requests)
            {
                var product = productCatalog.GetByName(itemRequest.ProductName);

                order.AddItem(product, itemRequest.Quantity);
            }

            orderRepository.Save(order);
        }
    }
}
using Exeal.Katas.TellDontAsk.Repository;

namespace Exeal.Katas.TellDontAsk.UseCase
{
    public class OrderApprovalUseCase
    {
        private readonly OrderRepository orderRepository;

        public OrderApprovalUseCase(OrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        public void Run(OrderApprovalRequest request)
        {
            var order = orderRepository.GetById(request.OrderId);

            order.SetOrderStatus(request);

            orderRepository.Save(order);
        }
    }
}
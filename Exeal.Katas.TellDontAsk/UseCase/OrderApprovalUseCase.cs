using Exeal.Katas.TellDontAsk.Domain;
using Exeal.Katas.TellDontAsk.Exception;
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
            Order order = orderRepository.GetById(request.OrderId);

            order.ApproveOrReject(request.Approved);
            orderRepository.Save(order);
        }
    }
}
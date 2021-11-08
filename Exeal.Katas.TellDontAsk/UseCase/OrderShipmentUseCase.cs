using Exeal.Katas.TellDontAsk.Domain;
using Exeal.Katas.TellDontAsk.Exception;
using Exeal.Katas.TellDontAsk.Repository;
using Exeal.Katas.TellDontAsk.Service;

namespace Exeal.Katas.TellDontAsk.UseCase
{
    public class OrderShipmentUseCase
    {
        private readonly OrderRepository orderRepository;
        private readonly ShipmentService shipmentService;

        public OrderShipmentUseCase(OrderRepository orderRepository, ShipmentService shipmentService)
        {
            this.orderRepository = orderRepository;
            this.shipmentService = shipmentService;
        }

        public void Run(OrderShipmentRequest request)
        {
            var order = orderRepository.GetById(request.OrderId);
            order.Ship(shipmentService);
            orderRepository.Save(order);
        }
    }
}

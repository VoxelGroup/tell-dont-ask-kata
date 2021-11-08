using System.Collections.Generic;
using Exeal.Katas.TellDontAsk.Exception;
using Exeal.Katas.TellDontAsk.UseCase;

namespace Exeal.Katas.TellDontAsk.Domain
{
    public class Order
    {
        public decimal Total { get; set; }
        public string Currency { get; set; }
        public List<OrderItem> Items { get; set; }
        public decimal Tax { get; set; }
        public OrderStatus Status { get; set; }
        public int Id { get; set; }

        public Order()
        {
            Items = new List<OrderItem>();
            Currency = "EUR";
            Total = 0M;
            Tax = 0M;
            Status = OrderStatus.Created;
        }

        public void ApproveOrRejectOrder(OrderApprovalRequest request)
        {
            if (Status.Equals(OrderStatus.Shipped))
            {
                throw new ShippedOrdersCannotBeChangedException();
            }

            if (request.Approved && Status.Equals(OrderStatus.Rejected))
            {
                throw new RejectedOrderCannotBeApprovedException();
            }

            if (!request.Approved && Status.Equals(OrderStatus.Approved))
            {
                throw new ApprovedOrderCannotBeRejectedException();
            }

            Status = request.Approved ? OrderStatus.Approved : OrderStatus.Rejected;
        }
    }
}
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

        public void ApproveOrReject(bool approved)
        {
            if (Status.Equals(OrderStatus.Shipped))
            {
                throw new ShippedOrdersCannotBeChangedException();
            }

            if (approved && Status.Equals(OrderStatus.Rejected))
            {
                throw new RejectedOrderCannotBeApprovedException();
            }

            if (!approved && Status.Equals(OrderStatus.Approved))
            {
                throw new ApprovedOrderCannotBeRejectedException();
            }

            Status = approved ? OrderStatus.Approved : OrderStatus.Rejected;
        }
    }
}
using System.Collections.Generic;
using Exeal.Katas.TellDontAsk.Exception;

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

        public Order(OrderStatus status, string currency)
        {
            Status = status;
            Currency = currency;
            Items = new List<OrderItem>();
            Total = 0M;
            Tax = 0M;
        }

        public void AddItem(OrderItem item)
        {
            Items.Add(item);

            Total += item.TaxedAmount;
            Tax += item.Tax;
        }

        public void ApproveOrReject(bool shouldApprove)
        {
            if (Status.Equals(OrderStatus.Shipped))
            {
                throw new ShippedOrdersCannotBeChangedException();
            }

            if (shouldApprove && Status.Equals(OrderStatus.Rejected))
            {
                throw new RejectedOrderCannotBeApprovedException();
            }

            if (!shouldApprove && Status.Equals(OrderStatus.Approved))
            {
                throw new ApprovedOrderCannotBeRejectedException();
            }

            Status = shouldApprove ? OrderStatus.Approved : OrderStatus.Rejected;
        }
    }
}
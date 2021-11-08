using Exeal.Katas.TellDontAsk.Exception;
using Exeal.Katas.TellDontAsk.Service;
using System.Collections.Generic;

namespace Exeal.Katas.TellDontAsk.Domain
{
    public class Order
    {
        public decimal Total { get; private set; }
        public string Currency { get; }
        public List<OrderItem> Items { get; }
        public decimal Tax { get; private set; }
        public OrderStatus Status { get; set; }
        public int Id { get; set; }

        public Order()
        {
            Status = OrderStatus.Created;
            Items = new List<OrderItem>();
            Currency = "EUR";
            Total = 0M;
            Tax = 0M;
        }

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

        public void AddItem(Product product, int quantity)
        {
            var orderItem = new OrderItem(product, quantity);
            
            Items.Add(orderItem); // OJO, temporal coupling

            Total += orderItem.TaxedAmount;
            Tax += orderItem.Tax;
        }

        public void Shipped()
        {
            if (Status.Equals(OrderStatus.Created) || Status.Equals(OrderStatus.Rejected))
            {
                throw new OrderCannotBeShippedException();
            }

            if (Status.Equals(OrderStatus.Shipped))
            {
                throw new OrderCannotBeShippedTwiceException();
            }

            Status = OrderStatus.Shipped;
        }
    }
}
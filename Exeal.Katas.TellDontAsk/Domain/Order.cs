using Exeal.Katas.TellDontAsk.Exception;
using Exeal.Katas.TellDontAsk.UseCase;
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

        public void AddItem(Product product, int quantity)
        {
            //if (product == null)
            //{
            //    throw new UnknownProductException();
            //}

            //if (quantity <= 0)
            //{
            //    throw new InvalidQuantityException();
            //}

            var orderItem = new OrderItem(product, quantity);


            Items.Add(orderItem);

            Total = Total + orderItem.TaxedAmount;
            Tax = Tax + orderItem.Tax;
        }
    }
}
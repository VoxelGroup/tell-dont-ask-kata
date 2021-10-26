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

        private void UpdateStatus(bool isRequestApproved)
        {
            Status = isRequestApproved ? OrderStatus.Approved : OrderStatus.Rejected;
        }

        private void CannotRejectIfAlreadyApproved(bool isRequestApproved)
        {
            if (!isRequestApproved && Status.Equals(OrderStatus.Approved))
            {
                throw new ApprovedOrderCannotBeRejectedException();
            }
        }

        private void MustNotBeRejected(bool isRequestApproved)
        {
            if (isRequestApproved && Status.Equals(OrderStatus.Rejected))
            {
                throw new RejectedOrderCannotBeApprovedException();
            }
        }

        private void MustNotBeShipped()
        {
            if (Status.Equals(OrderStatus.Shipped))
            {
                throw new ShippedOrdersCannotBeChangedException();
            }
        }

        public void Approve(bool isRequestApproved)
        {
            MustNotBeShipped();
            MustNotBeRejected(isRequestApproved);
            CannotRejectIfAlreadyApproved(isRequestApproved);
            UpdateStatus(isRequestApproved);
        }
    }
}
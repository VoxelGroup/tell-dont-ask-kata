using System;
using System.Collections.Generic;
using Exeal.Katas.TellDontAsk.Domain;
using Exeal.Katas.TellDontAsk.Exception;
using Exeal.Katas.TellDontAsk.Tests.Doubles;
using Exeal.Katas.TellDontAsk.UseCase;
using FluentAssertions;
using Xunit;

namespace Exeal.Katas.TellDontAsk.Tests.UseCase
{
    public class OrderApprovalUseCaseTest
    {
        private readonly TestOrderRepository orderRepository;
        private readonly OrderApprovalUseCase useCase;

        public OrderApprovalUseCaseTest()
        {
            orderRepository = new TestOrderRepository();
            useCase = new OrderApprovalUseCase(orderRepository);
        }

        [Fact]
        public void ApprovedExistingOrder()
        {
            Order initialOrder = new Order(0M, null, new List<OrderItem>(), 0M, OrderStatus.Created, 1);
            orderRepository.AddOrder(initialOrder);

            OrderApprovalRequest request = new OrderApprovalRequest();
            request.OrderId = 1;
            request.Approved = true;

            useCase.Run(request);

            Order savedOrder = orderRepository.GetSavedOrder();
            savedOrder.Status.Should().Be(OrderStatus.Approved);
        }

        [Fact]
        public void RejectedExistingOrder()
        {
            Order initialOrder = new Order(0M, null, new List<OrderItem>(), 0M, OrderStatus.Created, 1);
            orderRepository.AddOrder(initialOrder);

            OrderApprovalRequest request = new OrderApprovalRequest();
            request.OrderId = 1;
            request.Approved = false;

            useCase.Run(request);

            Order savedOrder = orderRepository.GetSavedOrder();
            savedOrder.Status.Should().Be(OrderStatus.Rejected);
        }

        [Fact]
        public void CannotApproveRejectedOrder()
        {
            Order initialOrder = new Order(0M, null, new List<OrderItem>(), 0M, OrderStatus.Rejected, 1);
            orderRepository.AddOrder(initialOrder);

            OrderApprovalRequest request = new OrderApprovalRequest();
            request.OrderId = 1;
            request.Approved = true;

            Action action = () => useCase.Run(request);

            action.Should().Throw<RejectedOrderCannotBeApprovedException>();
            orderRepository.GetSavedOrder().Should().BeNull();
        }

        [Fact]
        public void CannotRejectApprovedOrder()
        {
            Order initialOrder = new Order(0M, null, new List<OrderItem>(), 0M, OrderStatus.Approved, 1);
            orderRepository.AddOrder(initialOrder);

            OrderApprovalRequest request = new OrderApprovalRequest();
            request.OrderId = 1;
            request.Approved = false;

            Action action = () => useCase.Run(request);

            action.Should().Throw<ApprovedOrderCannotBeRejectedException>();
            orderRepository.GetSavedOrder().Should().BeNull();
        }

        [Fact]
        public void ShippedOrdersCannotBeApproved()
        {
            Order initialOrder = new Order(0M, null, new List<OrderItem>(), 0M, OrderStatus.Shipped, 1);
            orderRepository.AddOrder(initialOrder);

            OrderApprovalRequest request = new OrderApprovalRequest();
            request.OrderId = 1;
            request.Approved = true;

            Action action = () => useCase.Run(request);

            action.Should().Throw<ShippedOrdersCannotBeChangedException>();
            orderRepository.GetSavedOrder().Should().BeNull();
        }

        [Fact]
        public void ShippedOrdersCannotBeRejected()
        {
            Order initialOrder = new Order(0M, null, new List<OrderItem>(), 0M, OrderStatus.Shipped, 1);
            orderRepository.AddOrder(initialOrder);

            OrderApprovalRequest request = new OrderApprovalRequest();
            request.OrderId = 1;
            request.Approved = false;

            Action action = () => useCase.Run(request);

            action.Should().Throw<ShippedOrdersCannotBeChangedException>();
            orderRepository.GetSavedOrder().Should().BeNull();
        }
    }
}
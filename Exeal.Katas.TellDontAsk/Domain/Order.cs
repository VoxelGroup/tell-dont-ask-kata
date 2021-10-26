using System.Collections.Generic;

namespace Exeal.Katas.TellDontAsk.Domain
{
    public class Order
    {
        public Order(decimal total, string currency, List<OrderItem> items, decimal tax, OrderStatus status)
        {
            Total = total;
            Currency = currency;
            Items = items;
            Tax = tax;
            Status = status;
        }

        public Order(decimal total, string currency, List<OrderItem> items, decimal tax, OrderStatus status, int id)
        {
            Total = total;
            Currency = currency;
            Items = items;
            Tax = tax;
            Status = status;
            Id = id;
        }

        public decimal Total { get; set; }
        public string Currency { get; set; }
        public List<OrderItem> Items { get; set; }
        public decimal Tax { get; set; }
        public OrderStatus Status { get; set; }
        public int Id { get; set; }

        public void CalculateTotal(decimal taxedAmount)
        {
            Total += taxedAmount;
        }

        public void CalculateTax(decimal taxAmount)
        {
            Tax += taxAmount;
        }

        public void AddItem(OrderItem orderItem)
        {
            Items.Add(orderItem);
        }
    }
}
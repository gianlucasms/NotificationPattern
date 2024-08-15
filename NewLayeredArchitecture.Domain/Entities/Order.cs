using System.Collections.Generic;

namespace NewLayeredArchitecture.Domain.Entities;

public class Order
{
    public int OrderId { get; set; }
    public int CustomerId { get; set; }
    public Customer Customer { get; set; }
    public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
}


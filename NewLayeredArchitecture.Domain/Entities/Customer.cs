﻿using System.Collections.Generic;

namespace NewLayeredArchitecture.Domain.Entities;

public class Customer
{
    public int CustomerId { get; set; }
    public string Name { get; set; } 
    public ICollection<Order> Orders { get; set; } = new List<Order>();
}

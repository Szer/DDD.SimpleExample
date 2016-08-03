using System;
using System.Collections.Generic;
using DDD.SimpleExample.Common.Enums;

namespace DDD.SimpleExample.Common.DTOs
{
    public class Customer
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public CustomerStatus Status { get; set; }
        public List<Guid> ProjectIds { get; set; }
    }
}
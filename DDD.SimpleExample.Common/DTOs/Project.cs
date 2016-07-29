using System;

namespace DDD.SimpleExample.Common.DTOs
{
    public class Project
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public string Name { get; set; }
    }
}
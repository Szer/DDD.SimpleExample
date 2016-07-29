using System;

namespace DDD.SimpleExample.Application.API.Models
{
    public class AddCustomerResult
    {
        public Guid AddCustomerRequestId { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
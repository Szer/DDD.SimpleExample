using System;
using System.ComponentModel.DataAnnotations;

namespace DDD.SimpleExample.Application.API.Models
{
    public class AddCustomerRequest
    {
        public Guid AddCustomerRequestId { get; set; }

        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
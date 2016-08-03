using System;
using System.ComponentModel.DataAnnotations;

namespace DDD.SimpleExample.Application.API.Models.Customer
{
    public class AddCustomerModel
    {
        public Guid CommandId { get; set; }

        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
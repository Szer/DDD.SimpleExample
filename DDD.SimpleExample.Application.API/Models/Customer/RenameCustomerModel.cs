using System;
using System.ComponentModel.DataAnnotations;

namespace DDD.SimpleExample.Application.API.Models.Customer
{
    public class RenameCustomerModel
    {
        public Guid CommandId { get; set; }

        [Required]
        public string NewName { get; set; }
    }
}
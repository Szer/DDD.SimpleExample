using System;
using System.ComponentModel.DataAnnotations;

namespace DDD.SimpleExample.Application.API.Models.Project
{
    public class AddProjectToCustomerModel
    {
        public Guid CommandId { get; set; }

        [Required]
        public Guid ProjectId { get; set; }

        [Required]
        public Guid CustomerId { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
using System;
using System.ComponentModel.DataAnnotations;

namespace DDD.SimpleExample.Application.API.Models.Project
{
    public class RenameProjectModel
    {
        public Guid CommandId { get; set; }

        [Required]
        public string NewName { get; set; }
    }
}
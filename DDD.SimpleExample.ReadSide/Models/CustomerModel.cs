using System;
using System.Collections.Generic;
using DDD.SimpleExample.Common.Enums;
using DDD.SimpleExample.ReadSide.Helpers;

namespace DDD.SimpleExample.ReadSide.Models
{
    public class CustomerModel : BaseModel
    {
        public CustomerModel(Guid id) : base(id)
        {
        }

        public string Name { get; set; }
        public CustomerStatus Status { get; set; }
        public ICollection<ProjectModel> Projects { get; set; }
    }
}
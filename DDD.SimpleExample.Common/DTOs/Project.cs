using System;
using System.Collections.Generic;
using DDD.SimpleExample.Common.Enums;

namespace DDD.SimpleExample.Common.DTOs
{
    public class Project
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public string Name { get; set; }
        public ProjectStatus Status { get; set; }
        public List<Guid> AssignedUsersIds { get; set; }
    }
}
using System;
using System.Collections.Generic;
using DDD.SimpleExample.Common.Enums;
using DDD.SimpleExample.ReadSide.Helpers;

namespace DDD.SimpleExample.ReadSide.Models
{
    public class UserModel : BaseModel
    {
        private UserModel()
        {
        }
        public UserModel(Guid id) : base(id)
        {
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<ProjectModel> AssignedProjects { get; set; }
        public UserRole Role { get; set; }
    }
}
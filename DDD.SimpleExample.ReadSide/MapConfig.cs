using System.Linq;
using AutoMapper;
using DDD.SimpleExample.Common.DTOs;
using DDD.SimpleExample.ReadSide.Models;

namespace DDD.SimpleExample.ReadSide
{
    internal static class MapConfig
    {
        public static IMapper CreateMapper()
        {
            var config = new MapperConfiguration(c =>
            {
                c.CreateMap<CustomerModel, Customer>()
                    .ForMember(s => s.ProjectIds, opt => opt.MapFrom(src => src.Projects.Select(x => x.AggregateId)))
                    .ForMember(s => s.Id, opt => opt.MapFrom(src => src.AggregateId));
                c.CreateMap<ProjectModel, Project>()
                    .ForMember(s => s.CustomerId, opt => opt.MapFrom(src => src.CustomerModel.AggregateId))
                    .ForMember(s => s.AssignedUsersIds,
                        opt => opt.MapFrom(src => src.AssignedUsers.Select(x => x.AggregateId)))
                    .ForMember(s => s.Id, opt => opt.MapFrom(src => src.AggregateId));
            });
            config.AssertConfigurationIsValid();
            return config.CreateMapper();
        }
    }
}
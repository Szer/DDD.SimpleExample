using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DDD.SimpleExample.Common.DTOs;
using DDD.SimpleExample.Common.Queries.Project;
using DDD.SimpleExample.ReadSide.Interfaces;
using MassTransit;

namespace DDD.SimpleExample.ReadSide.Readers
{
    internal class ProjectReader :
        IConsumer<IGetAllProjects>,
        IConsumer<IGetProject>
    {
        private readonly IModelReader _reader;
        private readonly IMapper _mapper;

        public ProjectReader(IModelReader reader, IMapper mapper)
        {
            _reader = reader;
            _mapper = mapper;
        }

        public async Task Consume(ConsumeContext<IGetAllProjects> context)
        {
            var projects = _reader.Projects
                .Include(x => x.AssignedUsers)
                .Include(x => x.CustomerModel)
                .Select(_mapper.Map<Project>)
                .ToList();
            var respond = new GetAllProjectsResult(projects);
            await context.RespondAsync(respond);
        }

        public async Task Consume(ConsumeContext<IGetProject> context)
        {
            var project = _mapper.Map<Project>(_reader.Projects
                .Include(x => x.AssignedUsers)
                .Include(x => x.CustomerModel)
                .FirstOrDefault(x => x.AggregateId == context.Message.Id));
            var respond = new GetProjectResult(project);
            await context.RespondAsync(respond);
        }

        class GetAllProjectsResult : IGetAllProjectsResult
        {
            public GetAllProjectsResult(List<Project> projects)
            {
                Projects = projects;
            }

            public List<Project> Projects { get; }
        }

        class GetProjectResult : IGetProjectResult
        {
            public GetProjectResult(Project project)
            {
                Project = project;
            }

            public Project Project { get; }
        }
    }
}
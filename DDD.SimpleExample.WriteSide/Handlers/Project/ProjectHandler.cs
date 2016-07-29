using System.Threading.Tasks;
using DDD.SimpleExample.Common.Commands.Project;
using DDD.SimpleExample.Domain.Project.Service;
using MassTransit;

namespace DDD.SimpleExample.WriteSide.Handlers.Project
{
    internal class ProjectHandler :
        IConsumer<IAddProjectToCustomer>,
        IConsumer<IMakeProjectInActive>,
        IConsumer<IRenameProject>
    {
        private readonly IProjectService _service;

        public ProjectHandler(IProjectService service)
        {
            _service = service;
        }

        public async Task Consume(ConsumeContext<IAddProjectToCustomer> context)
        {
            await Task.Run(() => _service.AddProjectToCustomer(
                context.Message.Id,
                context.Message.Name,
                context.Message.CustomerGuid));
        }

        public async Task Consume(ConsumeContext<IMakeProjectInActive> context)
        {
            await Task.Run(() => _service.MakeInActive(context.Message.Id));
        }

        public async Task Consume(ConsumeContext<IRenameProject> context)
        {
            await Task.Run(() => _service.Rename(context.Message.Id, context.Message.NewName));
        }
    }
}
using System.Threading.Tasks;
using DDD.SimpleExample.Common.Enums;
using DDD.SimpleExample.Common.Events.Project;
using DDD.SimpleExample.ReadSide.Helpers;
using DDD.SimpleExample.ReadSide.Interfaces;
using DDD.SimpleExample.ReadSide.Models;
using MassTransit;

namespace DDD.SimpleExample.ReadSide.Updaters.Project
{
    public class ProjectUpdater :
        IConsumer<IProjectAdded>,
        IConsumer<IProjectRenamed>,
        IConsumer<IProjectMarkedAsInActive>
    {
        private readonly IModelUpdater _context;

        public ProjectUpdater(IModelUpdater context)
        {
            _context = context;
        }

        public async Task Consume(ConsumeContext<IProjectAdded> context)
        {
            var project = new ProjectModel(context.Message.Id)
            {
                Name = context.Message.Name,
                CustomerId = BaseModel.MakeId(typeof(CustomerModel), context.Message.CustomerId)
            };
            _context.Projects.Add(project);
            await _context.SaveChangesAsync();
        }

        public async Task Consume(ConsumeContext<IProjectRenamed> context)
        {
            var project = _context.Projects.FindAggregate(context.Message.Id);
            project.Name = context.Message.NewName;
            await _context.SaveChangesAsync();
        }

        public async Task Consume(ConsumeContext<IProjectMarkedAsInActive> context)
        {
            var project = _context.Projects.FindAggregate(context.Message.Id);
            project.Status = ProjectStatus.InActive;
            await _context.SaveChangesAsync();
        }
    }
}

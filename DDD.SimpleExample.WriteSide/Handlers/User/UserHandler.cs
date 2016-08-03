using System.Threading.Tasks;
using DDD.SimpleExample.Common.Commands.User;
using DDD.SimpleExample.Domain.User.Service;
using MassTransit;

namespace DDD.SimpleExample.WriteSide.Handlers.User
{
    internal class UserHandler :
        IConsumer<IAddUser>,
        IConsumer<IAssignUserToProject>,
        IConsumer<IPromoteUser>,
        IConsumer<IDemoteUser>
    {
        private readonly IUserService _service;

        public UserHandler(IUserService service)
        {
            _service = service;
        }

        public async Task Consume(ConsumeContext<IAddUser> context)
        {
            await Task.Run(() => _service.Add(context.Message.Id, context.Message.FirstName, context.Message.LastName));
        }

        public async Task Consume(ConsumeContext<IAssignUserToProject> context)
        {
            await Task.Run(() => _service.AssignToProject(context.Message.UserId, context.Message.ProjectId));
        }

        public async Task Consume(ConsumeContext<IPromoteUser> context)
        {
            await Task.Run(() => _service.Promote(context.Message.UserId));
        }

        public async Task Consume(ConsumeContext<IDemoteUser> context)
        {
            await Task.Run(() => _service.Demote(context.Message.UserId));
        }
    }
}
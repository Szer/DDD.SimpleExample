using System;
using System.Threading.Tasks;
using System.Web.Http;
using DDD.SimpleExample.Application.API.Models;
using DDD.SimpleExample.Application.API.Models.Project;
using DDD.SimpleExample.Common.Commands.Project;
using DDD.SimpleExample.Common.Queries.Project;
using MassTransit;

namespace DDD.SimpleExample.Application.API.Controllers
{
    public class ProjectController : EnhancedApiController
    {
        public async Task<IHttpActionResult> Post(AddProjectToCustomerModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (model.CommandId == Guid.Empty)
                model.CommandId = NewId.NextGuid();

            var command = new AddProjectToCustomerCommand(model);
            await Send(command);

            return Accepted(new PostResult<AddProjectToCustomerCommand>()
            {
                CommandId = model.ProjectId,
                Timestamp = command.Timestamp
            });
        }

        [HttpPost]
        [Route("api/project/{id}/makeinactive")]
        public async Task<IHttpActionResult> MakeInactive(Guid id)
        {
            var commandId = NewId.NextGuid();

            var command = new MakeProjectInActiveCommand(id, commandId);
            await Send(command);

            return Accepted(new PostResult<MakeProjectInActiveCommand>()
            {
                CommandId = id,
                Timestamp = command.Timestamp
            });
        }

        [HttpPost]
        [Route("api/project/{id}/name")]
        public async Task<IHttpActionResult> Rename(Guid id, RenameProjectModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (model.CommandId == Guid.Empty)
                model.CommandId = NewId.NextGuid();

            var command = new RenameProjectCommand(model, id);
            await Send(command);

            return Accepted(new PostResult<RenameProjectCommand>()
            {
                CommandId = id,
                Timestamp = command.Timestamp
            });
        }

        public async Task<IHttpActionResult> Get(Guid id)
        {
            if (id == Guid.Empty)
            {
                ModelState.AddModelError(nameof(id), "Cannot be empty");
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var query = new GetProject(id);
            var result = await SendRequest<IGetProject, IGetProjectResult>(query);
            return Ok(result);
        }

        public async Task<IHttpActionResult> Get()
        {
            var query = new GetAllProjects();
            var result = await SendRequest<IGetAllProjects, IGetAllProjectsResult>(query);
            return Ok(result);
        }
    }

    class GetAllProjects : IGetAllProjects
    {
    }

    class GetProject : IGetProject
    {
        public GetProject(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }

    class RenameProjectCommand : IRenameProject
    {
        private readonly RenameProjectModel _model;

        public RenameProjectCommand(RenameProjectModel model, Guid id)
        {
            _model = model;
            Timestamp = DateTime.UtcNow;
            Id = id;
        }

        public Guid Id { get; }
        public string NewName => _model.NewName;
        public DateTime Timestamp { get; }
        public Guid CommandId => _model.CommandId;
    }

    class AddProjectToCustomerCommand : IAddProjectToCustomer
    {
        private readonly AddProjectToCustomerModel _model;

        public AddProjectToCustomerCommand(AddProjectToCustomerModel model)
        {
            _model = model;
            Timestamp = DateTime.UtcNow;
        }

        public DateTime Timestamp { get; }
        public Guid CommandId => _model.CommandId;
        public Guid Id => _model.ProjectId;
        public string Name => _model.Name;
        public Guid CustomerId => _model.CustomerId;
    }

    class MakeProjectInActiveCommand : IMakeProjectInActive
    {
        public MakeProjectInActiveCommand(Guid id, Guid commandId)
        {
            Id = id;
            Timestamp = DateTime.UtcNow;
            CommandId = commandId;
        }

        public Guid Id { get; }
        public DateTime Timestamp { get; }
        public Guid CommandId { get; }
    }
}
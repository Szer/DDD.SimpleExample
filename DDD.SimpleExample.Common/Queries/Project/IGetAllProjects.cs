using System.Collections.Generic;

namespace DDD.SimpleExample.Common.Queries.Project
{
    public interface IGetAllProjects
    {
    }

    public interface IGetAllProjectsResult
    {
        List<DTOs.Project> Projects { get; }
    }
}
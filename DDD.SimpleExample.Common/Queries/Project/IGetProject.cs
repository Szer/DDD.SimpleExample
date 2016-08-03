using System;

namespace DDD.SimpleExample.Common.Queries.Project
{
    public interface IGetProject
    {
        Guid Id { get; }
    }

    public interface IGetProjectResult
    {
        DTOs.Project Project { get; }
    }
}
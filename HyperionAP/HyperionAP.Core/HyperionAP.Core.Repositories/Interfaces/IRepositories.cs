using HyperionAP.Core.Models;

namespace HyperionAP.Core.Repositories.Interfaces;

public interface IUserRepository : ICrudRepository<User>
{
    Task<IReadOnlyCollection<User>> GetAllUsersAsync();
}
public interface IProjectRepository : ICrudRepository<Project>
{
    Task<IReadOnlyCollection<Project>> GetAllProjectsAsync();
}
public interface IDatasetRepository : ICrudRepository<Dataset> { }
public interface IPipelineRepository : ICrudRepository<Pipeline> { }

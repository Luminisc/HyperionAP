using HyperionAP.Core.Models;
using HyperionAP.Core.Repositories.Interfaces;

namespace HyperionAP.Infrastructure.Repositories;
public class ProjectRepository(ApplicationDbContext context) : IProjectRepository
{
    public async Task<Project> AddAsync(Project entity)
    {
        var newUser = await context.AddAsync(entity);
        await context.SaveChangesAsync();
        return newUser.Entity;
    }

    public Task DeleteAsync(Project entity) => throw new NotImplementedException();
    public Task<Project> GetAsync(Guid id) => throw new NotImplementedException();
    public Task<Project> UpdateAsync(Project entity) => throw new NotImplementedException();
    public async Task<IReadOnlyCollection<Project>> GetAllProjectsAsync() => [.. context.Projects];
}

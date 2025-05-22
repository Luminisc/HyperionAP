using HyperionAP.Core.Models;
using HyperionAP.Core.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HyperionAP.Infrastructure.Repositories;

public class UserRepository(ApplicationDbContext context) : IUserRepository
{
    public async Task<User> AddAsync(User user)
    {
        var newUser = await context.AddAsync(user);
        await context.SaveChangesAsync();
        return newUser.Entity;
    }

    public Task DeleteAsync(User user) => throw new NotImplementedException();
    public async Task<User?> GetAsync(Guid id) => await context.Users.FirstOrDefaultAsync(x => x.Id == id);
    public Task<User> UpdateAsync(User user) => throw new NotImplementedException();
    public async Task<IReadOnlyCollection<User>> GetAllUsersAsync() => context.Users.ToList();
}

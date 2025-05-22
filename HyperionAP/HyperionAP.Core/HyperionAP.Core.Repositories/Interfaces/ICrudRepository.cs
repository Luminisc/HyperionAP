using HyperionAP.Core.Models.EntitiesBase;

namespace HyperionAP.Core.Repositories.Interfaces;

public interface ICrudRepository<T> where T : Entity
{
    Task<T?> GetAsync(Guid id);
    Task<T> AddAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task DeleteAsync(T entity);
}

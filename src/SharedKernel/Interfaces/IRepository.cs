using System.Linq.Expressions;

namespace SharedKernel.Interfaces
{
    public interface IRepository
    {
        Task<T?> GetByIdAsync<T>(Guid id) where T : BaseEntity;
        Task<List<T>> ListAsync<T>() where T : BaseEntity;
        Task<T> AddAsync<T>(T entity) where T : BaseEntity;
        Task<List<T>> AddRangeAsync<T>(List<T> list) where T : BaseEntity;
        Task UpdateAsync<T>(T entity) where T : BaseEntity;
        Task DeleteAsync<T>(T entity) where T : BaseEntity;
        Task<IEnumerable<T>> SearchAsync<T>(Expression<Func<T, bool>> expression) where T : BaseEntity;
    }
}
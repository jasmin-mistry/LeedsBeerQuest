using System.Linq.Expressions;
using Entities;
using Microsoft.EntityFrameworkCore;
using SharedKernel;
using SharedKernel.Interfaces;

namespace Infrastructure.Data
{
    public class EfRepository : IRepository
    {
        private readonly AppDbContext dbContext;

        public EfRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<T?> GetByIdAsync<T>(Guid id) where T : BaseEntity
        {
            return await dbContext.Set<T>().SingleOrDefaultAsync(e => e.Id == id);
        }

        public async Task<List<T>> ListAsync<T>() where T : BaseEntity
        {
            return await dbContext.Set<T>().ToListAsync();
        }

        public async Task<T> AddAsync<T>(T entity) where T : BaseEntity
        {
            await dbContext.Set<T>().AddAsync(entity);
            await dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task<List<T>> AddRangeAsync<T>(List<T> list) where T : BaseEntity
        {
            await dbContext.Set<T>().AddRangeAsync(list);
            await dbContext.SaveChangesAsync();
            return list;
        }

        public async Task UpdateAsync<T>(T entity) where T : BaseEntity
        {
            dbContext.Entry(entity).State = EntityState.Modified;
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync<T>(T entity) where T : BaseEntity
        {
            dbContext.Set<T>().Remove(entity);
            await dbContext.SaveChangesAsync();
        }
        
        public async Task<IEnumerable<T>> SearchAsync<T>(Expression<Func<T, bool>> expression) where T : BaseEntity
        {
            var list = await dbContext.Set<T>().AsQueryable().Where(expression).ToListAsync();
            
            return list.AsReadOnly();
        }
    }
}
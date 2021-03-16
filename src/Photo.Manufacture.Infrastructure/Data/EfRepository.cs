using System.Collections.Generic;
using System.Threading.Tasks;
using Photo.Manufacture.SharedKernel;
using Photo.Manufacture.SharedKernel.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Photo.Manufacture.Infrastructure.Data
{
    public class EfRepository<T> : IRepository<T> where T : BaseEntity, IAggregateRoot
    {
        protected readonly AppDbContext dbContext;

        public EfRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public virtual Task<T> GetByIdAsync(int id)
        {
            return dbContext.Set<T>().SingleOrDefaultAsync(e => e.Id == id);
        }

        public virtual Task<List<T>> ListAsync()
        {
            return dbContext.Set<T>().ToListAsync();
        }

        public virtual async Task<T> AddAsync(T entity)
        {
            await dbContext.Set<T>().AddAsync(entity);
            await dbContext.SaveChangesAsync();

            return entity;
        }

        public virtual Task UpdateAsync(T entity)
        {
            dbContext.Entry(entity).State = EntityState.Modified;
            return dbContext.SaveChangesAsync();
        }

        public virtual Task DeleteAsync(T entity)
        {
            dbContext.Set<T>().Remove(entity);
            return dbContext.SaveChangesAsync();
        }
    }
}

using EF_DAL.Context;
using EF_DAL.Model;
using Microsoft.EntityFrameworkCore;


namespace EF_DAL.Repositories
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        protected readonly TaskManagerContext Context;

        protected Repository(TaskManagerContext context)
        {
            Context = context;
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return (await GetAllAsync())
                .FirstOrDefault(e => e.Id == id);
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await Context
                .Set<TEntity>().ToListAsync();
        }
        public virtual async Task<bool> DeleteByIdAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity == null)
            {
                return false;
            }
            Context.Remove(entity);
            await Context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdateAsync(TEntity entity)
        {
            var dbEntity = await GetByIdAsync(entity.Id);
            if (dbEntity == null)
            {
                return false;
            }
            Context.Update(entity);
            await Context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> CreateAsync(TEntity entity)
        {
            await Context.AddAsync(entity);
            await Context.SaveChangesAsync();
            return true;
        }
        public async Task<int> CountAsync()
        {
            return await Context.Set<TEntity>().CountAsync();
        }
    }
}

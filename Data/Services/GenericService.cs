using CA02_ASP.NET_Core.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CA02_ASP.NET_Core.Data.Services
{

    public interface IGenericService<T> where T : class
    {
        Task<int> AddAsync(T entity);
        Task<T> GetByIdAsync(object id);
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "");
        Task<int> UpdateAsync(T entity);
        Task<int> DeleteByIdAsync(object id);
        Task<int> SaveAsync();
    }

    public class GenericService<EntityType> : IGenericService<EntityType>
        where EntityType : class
    {
        protected readonly Context _db;
        protected readonly DbSet<EntityType> _dbSet;

        public GenericService(Context context)
        {
            _db = context;
            _dbSet = context.Set<EntityType>();
        }

        public async Task<int> AddAsync(EntityType entity)
        {
            await _dbSet.AddAsync(entity);
            return await SaveAsync();
        }
        public async Task<int> DeleteByIdAsync(object id)
        {
            var entityToDelete = await _dbSet.FindAsync(id);

            if (entityToDelete != null)
            {
                _dbSet.Remove(entityToDelete);
                return await SaveAsync();
            }
            return 0;
        }
        public async Task<EntityType> GetByIdAsync(object id)
        {
            var result = await _dbSet.FindAsync(id);
            return result;
        }
        public async Task<int> UpdateAsync(EntityType entity)
        {
            _dbSet.Update(entity);
            return await SaveAsync();
        }
        public async Task<int> SaveAsync()
        {
            return await _db.SaveChangesAsync();
        }

        public async Task<List<EntityType>> GetAllAsync(Expression<Func<EntityType, bool>> filter = null, Func<IQueryable<EntityType>, IOrderedQueryable<EntityType>> orderBy = null, string includeProperties = "")
        {
            IQueryable<EntityType> query = _dbSet;

            if (filter != null) query = query.Where(filter);
            includeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList().ForEach(x => query = query.Include(x));

            if (orderBy != null) return await orderBy(query).ToListAsync();

            return await query.ToListAsync();
        }

       

    }

}

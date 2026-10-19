using System.Linq.Expressions;
using BelshefaaApp.Core.Entities;
using BelshefaaApp.Core.Interfaces;
using BelshefaaApp.Database.Data;
using Microsoft.EntityFrameworkCore;

namespace BelshefaaApp.Database.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : EntityBase
    {
        protected DbSet<T> DbSet { get; }

        public GenericRepository(AppDbContext context)
        {
            DbSet = context.Set<T>();
        }

        public async Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await DbSet.FindAsync([id], cancellationToken);
        }

        public async Task<T?> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await DbSet.FirstOrDefaultAsync(predicate, cancellationToken);
        }

        public async Task<IReadOnlyList<T>> ListAsync(CancellationToken cancellationToken = default)
        {
            return await DbSet.AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            await DbSet.AddAsync(entity, cancellationToken);
        }

        public void Update(T entity)
        {
            DbSet.Update(entity);
        }

        public void Remove(T entity)
        {
            DbSet.Remove(entity);
        }
    }
}

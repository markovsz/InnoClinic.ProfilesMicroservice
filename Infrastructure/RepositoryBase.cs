using Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure
{
    public abstract class RepositoryBase<TEntity> where TEntity : EntityBase
    {
        private RepositoryContext _context;

        public RepositoryBase(RepositoryContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(TEntity entity) => await _context.AddAsync(entity);

        public IQueryable<TEntity> GetAll(bool trackChanges = false)
        {
            var entities = _context.Set<TEntity>();
            return trackChanges ? entities : entities.AsNoTracking();
        }

        public IQueryable<TEntity> GetByCondition(Expression<Func<TEntity, bool>> expression, bool trackChanges = false)
        {
            var entities = _context.Set<TEntity>()
                .Where(expression);
            return trackChanges ? entities : entities.AsNoTracking();
        }

        public void Update(TEntity entity) => _context.Update(entity);

        public void Delete(TEntity entity) => _context.Remove(entity);
    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SkeletaDAL.GenericRepository
{
	public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
	{
		protected readonly DbContext _context;
		protected readonly DbSet<TEntity> _entities;

		public Repository(DbContext context)
		{
			_context = context;
			_entities = context.Set<TEntity>();
		}


		public virtual void Add(TEntity entity) => _entities.Add(entity);
		public virtual void AddRange(IEnumerable<TEntity> entities) => _entities.AddRange(entities);

		public virtual async Task<TEntity> GetAsync(int id) => await _entities.FindAsync(id);
		public virtual async Task<IEnumerable<TEntity>> GetAllAsync() => await _entities.ToListAsync();
		public virtual async Task<TEntity> GetSingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate) => await _entities.SingleOrDefaultAsync(predicate);

		public virtual void Remove(TEntity entity) => _entities.Remove(entity);
		public virtual void RemoveRange(IEnumerable<TEntity> entities) => _entities.RemoveRange(entities);

		public virtual void Update(TEntity entity) => _entities.Update(entity);
		public virtual void UpdateRange(IEnumerable<TEntity> entities) => _entities.UpdateRange(entities);

		public virtual async Task<int> CountAsync() => await _entities.CountAsync();
		public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate) => await _entities.AnyAsync(predicate);
		public virtual async Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate) => await _entities.Where(predicate).ToListAsync();
	}
}

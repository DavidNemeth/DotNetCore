using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

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

		public virtual TEntity Get(int id) => _entities.Find(id);
		public virtual IEnumerable<TEntity> GetAll() => _entities.ToList();
		public virtual TEntity GetSingleOrDefault(Expression<Func<TEntity, bool>> predicate) => _entities.SingleOrDefault(predicate);

		public virtual void Remove(TEntity entity) => _entities.Remove(entity);
		public virtual void RemoveRange(IEnumerable<TEntity> entities) => _entities.RemoveRange(entities);

		public virtual void Update(TEntity entity) => _entities.Update(entity);
		public virtual void UpdateRange(IEnumerable<TEntity> entities) => _entities.UpdateRange(entities);

		public virtual int Count() => _entities.Count();
		public bool Exists(Expression<Func<TEntity, bool>> predicate) => _entities.Any(predicate);
		public virtual IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate) => _entities.Where(predicate);
	}
}

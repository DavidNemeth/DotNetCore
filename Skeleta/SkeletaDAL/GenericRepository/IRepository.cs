using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SkeletaDAL.GenericRepository
{
	public interface IRepository<TEntity> where TEntity : class
	{
		//Create
		void Add(TEntity entity);
		void AddRange(IEnumerable<TEntity> entities);

		//Read
		TEntity Get(int id);
		IEnumerable<TEntity> GetAll();

		//Update
		void Update(TEntity entity);
		void UpdateRange(IEnumerable<TEntity> entities);

		//Delete
		void Remove(TEntity entity);
		void RemoveRange(IEnumerable<TEntity> entities);

		//search
		IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
		TEntity GetSingleOrDefault(Expression<Func<TEntity, bool>> predicate);

		int Count();
	}
}
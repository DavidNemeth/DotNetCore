using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SkeletaDAL.GenericRepository
{
	public interface IRepository<TEntity> where TEntity : class
	{
		//Create
		void Add(TEntity entity);
		void AddRange(IEnumerable<TEntity> entities);

		//Read
		Task<TEntity> GetAsync(int id);
		Task<IEnumerable<TEntity>> GetAllAsync();

		//Update
		void Update(TEntity entity);
		void UpdateRange(IEnumerable<TEntity> entities);

		//Delete
		void Remove(TEntity entity);
		void RemoveRange(IEnumerable<TEntity> entities);

		//search
		Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate);
		Task<TEntity> GetSingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
		Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate);

		Task<int> CountAsync();
	}
}
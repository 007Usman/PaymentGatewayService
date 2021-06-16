using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PaymentGatway.Infrastructure.Data;

namespace PaymentGatway.Infrastructure.Repositories.Base
{
	public abstract class BaseRepository<TEntity> : IRepository<TEntity>
		where TEntity : class
	{
		protected PaymentGatewayDBContext _dbContext;

		public BaseRepository(PaymentGatewayDBContext dbContext)
		{
			_dbContext = dbContext;
		}

		/// <summary>
		/// Add a record
		/// </summary>
		/// <param name="entity"></param>
		/// <returns>TEntity</returns>
		public virtual async Task<TEntity> AddAsync(TEntity entity)
		{
			_dbContext.Set<TEntity>().Add(entity);
			await _dbContext.SaveChangesAsync();
			return entity;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="entities"></param>
		/// <returns></returns>
		public virtual async Task<IEnumerable<TEntity>> AddAsync(IEnumerable<TEntity> entities)
		{
			await _dbContext.Set<TEntity>().AddRangeAsync(entities);
			await _dbContext.SaveChangesAsync();
			return entities;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="entity"></param>
		/// <returns></returns>
		public virtual async Task DeleteAsync(TEntity entity)
		{
			_dbContext.Set<TEntity>().Remove(entity);
			await _dbContext.SaveChangesAsync();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
		{
			return await _dbContext.Set<TEntity>().ToListAsync();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="predicate"></param>
		/// <returns></returns>
		public virtual TEntity GetEntity(Expression<Func<TEntity, bool>> predicate)
		{
			return _dbContext.Set<TEntity>().Where(predicate).ToList().FirstOrDefault();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public virtual async Task<TEntity> GetByIdAsync(Guid id)
		{
			return await _dbContext.Set<TEntity>().FindAsync(id);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="entity"></param>
		/// <returns></returns>
		public virtual async Task<TEntity> UpdateAsync(Guid id, TEntity entity)
		{
			var data = await _dbContext.Set<TEntity>().FindAsync(id);
			_dbContext.Update(data);
			await _dbContext.SaveChangesAsync();
			return entity;
		}
	}
}

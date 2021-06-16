using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PaymentGatway.Infrastructure.Repositories.Base
{
	public interface IRepository<TEntity>
	{
		#region ReadOperations

		Task<TEntity> GetByIdAsync(Guid id);
		Task<IEnumerable<TEntity>> GetAllAsync();
		TEntity GetEntity(Expression<Func<TEntity, bool>> predicate);

		#endregion

		#region WriteOperation

		Task<TEntity> AddAsync(TEntity entity);
		Task<IEnumerable<TEntity>> AddAsync(IEnumerable<TEntity> entities);
		Task<TEntity> UpdateAsync(Guid id, TEntity entity);
		Task DeleteAsync(TEntity entity);

		#endregion
	}
}

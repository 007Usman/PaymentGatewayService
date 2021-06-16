using System;
using System.Threading.Tasks;
using PaymentGatway.Core.Models;
using PaymentGatway.Infrastructure.Data;
using PaymentGatway.Infrastructure.Repositories.Base;

namespace PaymentGatway.Infrastructure.Repositories
{
	public class BankRepository : BaseRepository<Bank>
	{
		public BankRepository(PaymentGatewayDBContext dbContext) : base(dbContext)
		{
		}

		/// <summary>
		/// Update transactional properties before adding data 
		/// </summary>
		/// <param name="entity"></param>
		/// <returns></returns>
		public override Task<Bank> AddAsync(Bank entity)
		{
			entity.BankId = Guid.NewGuid();
			entity.CreatedTimestamp = DateTimeOffset.Now;
			entity.ModifiedTimestamp = DateTimeOffset.Now;

			return base.AddAsync(entity);
		}
	}
}

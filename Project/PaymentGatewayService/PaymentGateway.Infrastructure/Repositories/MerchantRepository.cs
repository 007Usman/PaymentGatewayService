using System;
using System.Threading.Tasks;
using PaymentGatway.Core.Models;
using PaymentGatway.Infrastructure.Data;
using PaymentGatway.Infrastructure.Repositories.Base;

namespace PaymentGatway.Infrastructure.Repositories
{
	public class MerchantRepository : BaseRepository<Merchant>
	{
		public MerchantRepository(PaymentGatewayDBContext dbContext) : base(dbContext)
		{
		}

		/// <summary>
		/// Update transactional properties before adding data 
		/// </summary>
		/// <param name="entity"></param>
		/// <returns></returns>
		public override Task<Merchant> AddAsync(Merchant entity)
		{
			entity.MerchantId = Guid.NewGuid();
			entity.CreatedTimestamp = DateTimeOffset.Now;
			entity.ModifiedTimestamp = DateTimeOffset.Now;

			return base.AddAsync(entity);
		}
	}
}

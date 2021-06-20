using PaymentGatway.Core.Models;
using PaymentGatway.Infrastructure.Data;
using PaymentGatway.Infrastructure.Repositories.Base;

namespace PaymentGatway.Infrastructure.Repositories
{
	public class BankCardRepository : BaseRepository<BankCard>
	{
		public BankCardRepository(PaymentGatewayDBContext dbContext) : base(dbContext)
		{
		}
	}
}

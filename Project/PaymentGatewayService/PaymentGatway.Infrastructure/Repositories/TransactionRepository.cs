using PaymentGatway.Core.Models;
using PaymentGatway.Infrastructure.Data;
using PaymentGatway.Infrastructure.Repositories.Base;

namespace PaymentGatway.Infrastructure.Repositories
{
	public class TransactionRepository : BaseRepository<Transaction>
	{
		public TransactionRepository(PaymentGatewayDBContext dbContext) : base(dbContext)
		{
		}
	}
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using PaymentGatway.Infrastructure.Data;

namespace PaymentGatwayApiTests.Data
{
	public class PaymentGatewayDBContextUnitTests
	{
		public static PaymentGatewayDBContext SetupInMemoryDatabase(string databaseName)
		{
			var options = new DbContextOptionsBuilder<PaymentGatewayDBContext>()
							.UseInMemoryDatabase(databaseName)
							.ConfigureWarnings(s =>
							{
								s.Ignore(InMemoryEventId.TransactionIgnoredWarning);
							})
							.Options;


			var _dBContext = new PaymentGatewayDBContext(options);
			return _dBContext;
		}
	}
}

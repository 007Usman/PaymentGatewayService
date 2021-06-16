using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PaymentGatway.Infrastructure.Data;

namespace PaymentGatwayApiTests.Data
{
	public class PaymentGatewayDBContextUnitTests
	{
		public static PaymentGatewayDBContext SetupInMemoryDatabase(string databaseName)
		{
			var options = new DbContextOptionsBuilder<PaymentGatewayDBContext>()
							.UseInMemoryDatabase(databaseName)
							.Options;

			var _dBContext = new PaymentGatewayDBContext(options);

			return _dBContext;
		}
	}
}

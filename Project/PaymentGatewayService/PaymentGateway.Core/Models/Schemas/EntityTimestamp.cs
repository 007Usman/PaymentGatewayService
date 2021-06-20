using System;

namespace PaymentGatway.Core.Models
{
	public class EntityTimestamp : IEntityTimestamp
	{
		public DateTimeOffset CreatedTimestamp { get; set; }
		public DateTimeOffset ModifiedTimestamp { get; set; }
	}


}



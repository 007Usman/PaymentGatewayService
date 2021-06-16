using System;

namespace PaymentGatway.Core.Models
{
	public interface IEntityTimestamp
	{
		public DateTimeOffset CreatedTimestamp { get; set; }
		public DateTimeOffset ModifiedTimestamp { get; set; }
	}
}



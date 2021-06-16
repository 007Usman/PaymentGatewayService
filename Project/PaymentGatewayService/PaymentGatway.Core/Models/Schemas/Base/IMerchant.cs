using System;

namespace PaymentGatway.Core.Models
{
	public interface IMerchant : IEntityTimestamp
	{
		public Guid MerchantId { get; set; }
		public string MerchantName { get; set; }
		public string Description { get; set; }
	}
}



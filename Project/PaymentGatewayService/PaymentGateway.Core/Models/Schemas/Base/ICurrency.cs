using System;

namespace PaymentGatway.Core.Models
{
	public interface ICurrency : IEntityTimestamp
	{
		public Guid CurrencyId { get; set; }
		public string CurrencyCode { get; set; }
		public string CurrencyDescription { get; set; }
	}
}



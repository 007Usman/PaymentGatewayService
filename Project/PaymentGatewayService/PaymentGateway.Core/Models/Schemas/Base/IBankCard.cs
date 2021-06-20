using System;

namespace PaymentGatway.Core.Models
{
	public interface IBankCard : IEntityTimestamp
	{
		public Guid BankCardId { get; set; }
		public string CardNumber { get; set; }
		public int ExpiryMonth { get; set; }
		public int ExpiryYear { get; set; }
		public string CardHolderName { get; set; }
		public int CVV { get; set; }
	}
}



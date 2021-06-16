using System;

namespace PaymentGatway.Core.Models
{
	public class BankResponseRepresentor : EntityTimestamp
	{
		public string Currency { get; set; }
		public Guid BankResponeId { get; set; }
		public BankCard BankCardDetail { get; set; }
		public string TransactionStatus { get; set; }
		public Guid TransactionId { get; set; }

	}


}



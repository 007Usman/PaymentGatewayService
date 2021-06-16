using System;

namespace PaymentGatway.Core.Models
{
	public class TransactionAuditRequest : IBankCard
	{
		public decimal Amount { get; set; }
		public string TransactionStatus { get; set; }
		public Guid BankResponseId { get; set; }
		public Guid BankCardId { get; set; }
		public string CardNumber { get; set; }
		public int ExpiryMonth { get; set; }
		public int ExpiryYear { get; set; }
		public string CardHolderName { get; set; }
		public int CVV { get; set; }
		public DateTimeOffset CreatedTimestamp { get; set; }
		public DateTimeOffset ModifiedTimestamp { get; set; }
	}


}



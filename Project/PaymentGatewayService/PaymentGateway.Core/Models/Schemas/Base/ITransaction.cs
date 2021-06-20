using System;

namespace PaymentGatway.Core.Models
{
	public interface ITransaction : IEntityTimestamp
	{
		public Guid TransactionId { get; set; }
		public decimal Amount { get; set; }
		public string TransactionStatus { get; set; }
		public Guid BankReferenceId { get; set; }
	}
}



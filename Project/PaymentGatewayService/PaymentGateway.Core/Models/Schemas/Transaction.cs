using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaymentGatway.Core.Models
{
	public class Transaction : ITransaction
	{
		[Key]
		public Guid TransactionId { get; set; }

		[Column(TypeName = "decimal(18,2)")]
		public decimal Amount { get; set; }

		public string TransactionStatus { get; set; }
		public Guid BankReferenceId { get; set; }
		public Guid MerchantId { get; set; }
		public Guid BankId { get; set; }
		public Guid BankCardId { get; set; }
		public Guid CurrencyId { get; set; }
		public DateTimeOffset CreatedTimestamp { get; set; }
		public DateTimeOffset ModifiedTimestamp { get; set; }
	}
}



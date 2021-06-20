using System;

namespace PaymentGatway.Core.Models
{
	public interface IMerchantResponseRepresenter
	{
		public Guid BankResponseId { get; set; }
		public string TransactionStatus { get; set; }
	}
}



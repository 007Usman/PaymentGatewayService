using System;

namespace PaymentGatway.Core.Models
{
	public class MerchantResponseRepresenter : IMerchantResponseRepresenter
	{
		public Guid BankResponseId { get; set; }
		public string TransactionStatus { get; set; }
	}
}



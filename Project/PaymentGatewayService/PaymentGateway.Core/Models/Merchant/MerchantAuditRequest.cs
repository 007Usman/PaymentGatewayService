namespace PaymentGatway.Core.Models
{
	public class MerchantAuditRequest : TransactionAuditRequest
	{
		public Merchant Merchant { get; set; }
	}

}



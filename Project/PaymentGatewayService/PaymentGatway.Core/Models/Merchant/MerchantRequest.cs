using PaymentGatway.Core.Validations;

namespace PaymentGatway.Core.Models
{
	[MerchantRequestValidator]
	public class MerchantRequest
	{
		public string MerchantName { get; set; }
		public string BankName { get; set; }
		public string CurrencyCode { get; set; }
		public decimal Amount { get; set; }
		public string CardNumber { get; set; }
		public int ExpiryMonth { get; set; }
		public int ExpiryYear { get; set; }
		public string CardHolderName { get; set; }
		public int CVV { get; set; }
	}
}



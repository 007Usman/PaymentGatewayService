using PaymentGatway.Core.Validations;

namespace PaymentGatway.Core.Models
{
	[BankTransactionRegistorValidator]
	public class BankTransactionRegister
	{
		public decimal TransactionAmount { get; set; }
		public string CardNumber { get; set; }
		public int CVV { get; set; }
		public string CardHolderName { get; set; }
		public int ExpiryMonth { get; set; }
		public int ExpiryYear { get; set; }
	}
}



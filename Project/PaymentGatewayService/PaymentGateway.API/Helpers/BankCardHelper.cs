namespace PaymentGateway.API.Helpers
{
	public class BankCardHelper
	{
		public static string MaskCardNumber(string cardNumber)
		{
			string lastDigits = cardNumber.Substring(cardNumber.Length - 4, 4);
			string mask = new('*', cardNumber.Length - lastDigits.Length);

			return string.Concat(mask, lastDigits);

		}
	}
}

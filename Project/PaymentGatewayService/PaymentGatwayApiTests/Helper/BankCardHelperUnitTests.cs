using PaymentGateway.API.Helpers;
using Xunit;

namespace PaymentGatwayApiTests.Helper
{
	public class BankCardHelperUnitTests
	{
		[Fact]
		public void MaskCardNumber_RevealsLastFourDigits_ReturnStrings()
		{
			// Arrange
			string cardNumber = "1234567890123456";
			string expected = "************3456";

			// Act
			var actual = BankCardHelper.MaskCardNumber(cardNumber);

			// Assert
			Assert.Equal(expected, actual);

		}
	}
}

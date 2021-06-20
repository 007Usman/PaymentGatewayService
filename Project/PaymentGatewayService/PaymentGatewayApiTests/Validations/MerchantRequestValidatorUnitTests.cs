using PaymentGatway.Core.Models;
using PaymentGatway.Core.Validations;
using Xunit;

namespace PaymentGatwayApiTests.Validations
{
	public class MerchantRequestValidatorUnitTests
	{
		public readonly MerchantRequest _merchant;

		public MerchantRequestValidatorUnitTests()
		{
			_merchant = new MerchantRequest()
			{
				Amount = 5000,
				CardNumber = "1234567890123456",
				CVV = 908,
				ExpiryYear = 21,
				ExpiryMonth = 08
			};
		}

		/// <summary>
		/// Ensure card validated without any error
		/// </summary>
		/// <param name="amount"></param>
		[Theory]
		[InlineData(200.00, 08, 22)]
		[InlineData(50000.00, 12, 25)]
		[InlineData(1.00, 7, 23)]
		public void ValidateCard_CardDetailValidated_ReturnNoError(decimal amount, int month, int year)
		{
			// Arrange
			_merchant.Amount = amount;
			_merchant.ExpiryMonth = month;
			_merchant.ExpiryYear = year;

			// Act
			var actual = MerchantRequestValidator.ValidateCard(_merchant);

			// Assert
			Assert.Equal("", actual);
		}

		/// <summary>
		/// Provide failed validation message for all checks
		/// </summary>
		/// <param name="amount"></param>
		[Fact]
		public void ValidateCard_CardDetailValidated_ReturnErrorMessage()
		{
			// Arrange
			var expected = "Please provide a valid amount. " +
				"Please provide a valid card number. " +
				"Please provide a valid security code. " +
				"Please provide valid expiry month. ";

			_merchant.Amount = 0;
			_merchant.ExpiryMonth = 03;
			_merchant.CVV = 12;
			_merchant.CardNumber = "123456789012345";

			// Act
			var actual = MerchantRequestValidator.ValidateCard(_merchant);

			// Assert
			Assert.Equal(expected, actual);
		}

		/// <summary>
		///  Provide failed validation message for invalid amount
		/// </summary>
		/// <param name="amount"></param>
		[Theory]
		[InlineData(-1)]
		[InlineData(0)]
		[InlineData(-100)]
		public void ValidateCard_AmountValidated_ReturnErrorMessage(int amount)
		{
			// Arrange
			var expected = "Please provide a valid amount. ";
			_merchant.Amount = amount;

			// Act
			var actual = MerchantRequestValidator.ValidateCard(_merchant);

			// Assert
			Assert.Equal(expected, actual);
		}

		/// <summary>
		/// Provide failed validation message for invalid card number
		/// </summary>
		/// <param name="cardNumber"></param>
		[Theory]
		[InlineData("")]
		[InlineData("12456")]
		[InlineData("123456789012344567")]
		public void ValidateCard_CardNumberValidated_ReturnErrorMessage(string cardNumber)
		{
			// Arrange
			var expected = "Please provide a valid card number. ";
			_merchant.CardNumber = cardNumber;

			// Act
			var actual = MerchantRequestValidator.ValidateCard(_merchant);

			// Assert
			Assert.Equal(expected, actual);
		}

		/// <summary>
		/// Provide failed validation message for invalid security code
		/// </summary>
		/// <param name="cvv"></param>
		[Theory]
		[InlineData(03)]
		[InlineData(-45)]
		[InlineData(1234)]
		[InlineData(0)]
		public void ValidateCard_CVVValidated_ReturnErrorMessage(int cvv)
		{
			// Arrange
			var expected = "Please provide a valid security code. ";
			_merchant.CVV = cvv;

			// Act
			var actual = MerchantRequestValidator.ValidateCard(_merchant);

			// Assert
			Assert.Equal(expected, actual);
		}


		/// <summary>
		/// Provide failed validation message for invalid expiry year
		/// </summary>
		/// <param name="month"></param>
		/// <param name="year"></param>
		[Theory]
		[InlineData(20)]
		[InlineData(19)]
		[InlineData(-18)]
		public void ValidateCard_ExpiryYearValidated_ReturnErrorMessage(int year)
		{
			// Arrange
			var expected = "Please provide valid expiry year. ";
			_merchant.ExpiryYear = year;

			// Act
			var actual = MerchantRequestValidator.ValidateCard(_merchant);

			// Assert
			Assert.Equal(expected, actual);
		}


		/// <summary>
		/// Provide failed validation message for invalid expiry month
		/// </summary>
		/// <param name="month"></param>
		/// <param name="year"></param>
		[Theory]
		[InlineData(2)]
		[InlineData(05)]
		[InlineData(-2)]
		public void ValidateCard_ExpiryMonthValidated_ReturnErrorMessage(int month)
		{
			// Arrange
			var expected = "Please provide valid expiry month. ";
			_merchant.ExpiryMonth = month;

			// Act
			var actual = MerchantRequestValidator.ValidateCard(_merchant);

			// Assert
			Assert.Equal(expected, actual);
		}
	}
}

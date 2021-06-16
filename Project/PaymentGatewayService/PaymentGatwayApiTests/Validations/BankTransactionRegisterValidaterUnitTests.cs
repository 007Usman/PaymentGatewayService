using PaymentGatway.Core.Models;
using PaymentGatway.Core.Validations;
using Xunit;

namespace PaymentGatwayApiTests.Validations
{
	public class BankTransactionRegisterValidaterUnitTests
	{
		#region Properties 

		public readonly BankTransactionRegister _transactionRegister;

		#endregion

		#region Constructor

		public BankTransactionRegisterValidaterUnitTests()
		{
			_transactionRegister = new BankTransactionRegister()
			{
				TransactionAmount = 500,
				CardHolderName = "Joe Bloggs",
				CardNumber = "1234567890123456",
				CVV = 908,
				ExpiryYear = 21,
				ExpiryMonth = 08
			};
		}

		#endregion

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
			_transactionRegister.TransactionAmount = amount;
			_transactionRegister.ExpiryMonth = month;
			_transactionRegister.ExpiryYear = year;

			// Act
			var actual = BankTransactionRegistorValidator.ValidateCard(_transactionRegister);

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

			_transactionRegister.TransactionAmount = 0;
			_transactionRegister.ExpiryMonth = 03;
			_transactionRegister.CVV = 12;
			_transactionRegister.CardNumber = "123456789012345";

			// Act
			var actual = BankTransactionRegistorValidator.ValidateCard(_transactionRegister);

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
			_transactionRegister.TransactionAmount = amount;

			// Act
			var actual = BankTransactionRegistorValidator.ValidateCard(_transactionRegister);

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
			_transactionRegister.CardNumber = cardNumber;

			// Act
			var actual = BankTransactionRegistorValidator.ValidateCard(_transactionRegister);

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
			_transactionRegister.CVV = cvv;

			// Act
			var actual = BankTransactionRegistorValidator.ValidateCard(_transactionRegister);

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
			_transactionRegister.ExpiryYear = year;

			// Act
			var actual = BankTransactionRegistorValidator.ValidateCard(_transactionRegister);

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
			_transactionRegister.ExpiryMonth = month;

			// Act
			var actual = BankTransactionRegistorValidator.ValidateCard(_transactionRegister);

			// Assert
			Assert.Equal(expected, actual);
		}
	}
}

using System;
using System.ComponentModel.DataAnnotations;
using PaymentGatway.Core.Models;

namespace PaymentGatway.Core.Validations
{
	public class BankTransactionRegistorValidator : ValidationAttribute
	{
		/// <summary>
		/// Creates custom validation from override model state  
		/// </summary>
		/// <param name="value"></param>
		/// <param name="validationContext"></param>
		/// <returns></returns>
		protected override ValidationResult IsValid(object value,
			ValidationContext validationContext)
		{
			var bankTransaction = (BankTransactionRegister)validationContext.ObjectInstance;

			var errorMessages = ValidateCard(bankTransaction);
			if (errorMessages.Length > 0)
				return new ValidationResult(errorMessages);

			return ValidationResult.Success;
		}

		/// <summary>
		/// Valid Card Details
		/// </summary>
		/// <param name="bankTransaction"></param>
		/// <returns></returns>
		public static string ValidateCard(BankTransactionRegister bankTransaction)
		{
			var message = string.Empty;
			if (bankTransaction.TransactionAmount <= 0)
				message += "Please provide a valid amount. ";

			var cardNumber = bankTransaction.CardNumber.Length;
			if (cardNumber < 16 || cardNumber > 16)
				message += "Please provide a valid card number. ";

			int cvv = bankTransaction.CVV.ToString().Length;
			if ((cvv < 3) || (cvv > 3) || (bankTransaction.CVV <= 0))
				message += "Please provide a valid security code. ";


			var expirydate = ValidateCardExpiry(bankTransaction);
			if (!string.IsNullOrEmpty(expirydate))
				message += expirydate;

			return message;
		}

		/// <summary>
		/// Validates Card Expiry
		/// </summary>
		/// <param name="bankTransaction"></param>
		/// <returns></returns>
		private static string ValidateCardExpiry(BankTransactionRegister bankTransaction)
		{
			var expiryYearsLeft = bankTransaction.ExpiryYear - Convert.ToInt32(DateTime.Now.Year.ToString().Substring(2, 2));
			var currentMonth = Convert.ToInt32(DateTime.Now.Month.ToString().PadLeft(2, '0'));

			string message = string.Empty;
			if (expiryYearsLeft < 0)
			{
				message += "Please provide valid expiry year. ";
			}
			else
			{
				var expiryMonthsleft = bankTransaction.ExpiryMonth - currentMonth;
				if (expiryMonthsleft < 0)
					message += "Please provide valid expiry month. ";
			}

			return message;
		}
	}
}
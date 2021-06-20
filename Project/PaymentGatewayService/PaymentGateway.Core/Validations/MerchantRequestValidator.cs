using System;
using System.ComponentModel.DataAnnotations;
using PaymentGatway.Core.Models;

namespace PaymentGatway.Core.Validations
{
	public class MerchantRequestValidator : ValidationAttribute
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
			var mercant = (MerchantRequest)validationContext.ObjectInstance;

			var errorMessages = ValidateCard(mercant);
			if (errorMessages.Length > 0)
				return new ValidationResult(errorMessages);

			return ValidationResult.Success;
		}

		/// <summary>
		/// Valid Card Details
		/// </summary>
		/// <param name="merchant"></param>
		/// <returns></returns>
		public static string ValidateCard(MerchantRequest merchant)
		{
			var message = string.Empty;
			if (merchant.Amount <= 0)
				message += "Please provide a valid amount. ";

			var cardNumber = merchant.CardNumber.Length;
			if (cardNumber < 16 || cardNumber > 16)
				message += "Please provide a valid card number. ";

			int cvv = merchant.CVV.ToString().Length;
			if ((cvv < 3) || (cvv > 3) || (merchant.CVV <= 0))
				message += "Please provide a valid security code. ";


			var expirydate = ValidateCardExpiry(merchant);
			if (!string.IsNullOrEmpty(expirydate))
				message += expirydate;

			return message;
		}

		/// <summary>
		/// Validates Card Expiry
		/// </summary>
		/// <param name="merchant"></param>
		/// <returns></returns>
		private static string ValidateCardExpiry(MerchantRequest merchant)
		{
			var expiryYearsLeft = merchant.ExpiryYear - Convert.ToInt32(DateTime.Now.Year.ToString().Substring(2, 2));
			var currentMonth = Convert.ToInt32(DateTime.Now.Month.ToString().PadLeft(2, '0'));

			string message = string.Empty;
			if (expiryYearsLeft < 0)
			{
				message += "Please provide valid expiry year. ";
			}
			else
			{
				var expiryMonthsleft = merchant.ExpiryMonth - currentMonth;
				if (expiryMonthsleft < 0)
					message += "Please provide valid expiry month. ";
			}

			return message;
		}
	}
}
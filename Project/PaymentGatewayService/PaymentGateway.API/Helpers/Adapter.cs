using System;
using Newtonsoft.Json;
using PaymentGatway.Core.Encryption;
using PaymentGatway.Core.Models;

namespace PaymentGateway.API.Helpers
{
	public static class Adapter
	{
		/// <summary>
		/// Reads bank response while Deserializing response
		/// </summary>
		/// <param name="response"></param>
		/// <returns></returns>
		public static BankResponseRepresentor MapToBankResponseRepresentor(this string response)
		{
			var message = JsonConvert.DeserializeObject<MerchantResponseRepresenter>(response);

			return new BankResponseRepresentor
			{
				BankResponeId = message.BankResponseId,
				TransactionStatus = message.TransactionStatus,
				CreatedTimestamp = DateTimeOffset.Now,
				ModifiedTimestamp = DateTimeOffset.Now
			};

		}


		/// <summary>
		/// Prepare payment details, ready to post to the bank
		/// </summary>
		/// <param name="transaction"></param>
		/// <returns></returns>
		public static PaymentRepresentor MapToPaymentRepresentor(this MerchantRequest transaction)
		{
			return new PaymentRepresentor
			{
				TransactionAmount = transaction.Amount,
				CardNumber = transaction.CardNumber,
				CVV = transaction.CVV,
				CardHolderName = transaction.CardHolderName,
				ExpiryMonth = transaction.ExpiryMonth,
				ExpiryYear = transaction.ExpiryYear
			};
		}

		/// <summary>
		/// Map Card details recived from database or merchant
		/// Encrypt CardNumber to securely ingest data
		/// </summary>
		/// <param name="merchant"></param>
		/// <param name="card"></param>
		/// <param name="encryptor"></param>
		/// <returns></returns>
		public static BankCard MapToBankCard(this MerchantRequest merchant, BankCard card, IStringEncryptor encryptor)
		{
			string cardNumber;
			if (card == null)
				cardNumber = encryptor.Encrypt(merchant.CardNumber);
			else
				cardNumber = card.CardNumber;

			return new BankCard
			{
				BankCardId = Guid.NewGuid(),
				CardNumber = cardNumber,
				CardHolderName = merchant.CardHolderName,
				CVV = merchant.CVV,
				ExpiryYear = merchant.ExpiryYear,
				ExpiryMonth = merchant.ExpiryMonth,
				CreatedTimestamp = DateTimeOffset.Now,
				ModifiedTimestamp = DateTimeOffset.Now
			};
		}


		/// <summary>
		/// Map transaction as a result of receiving response from bank
		/// </summary>
		/// <param name="request"></param>
		/// <param name="representer"></param>
		/// <param name="merchant"></param>
		/// <param name="bank"></param>
		/// <param name="bankCard"></param>
		/// <param name="currency"></param>
		/// <returns></returns>
		public static Transaction MapToTransaction(this MerchantRequest request,
			BankResponseRepresentor representer,
			Merchant merchant,
			Bank bank,
			BankCard bankCard,
			Currency currency
			)
		{
			return new Transaction
			{
				TransactionId = representer.TransactionId,
				Amount = request.Amount,
				TransactionStatus = representer.TransactionStatus,
				MerchantId = merchant.MerchantId,
				BankId = bank.BankId,
				BankCardId = bankCard.BankCardId,
				CurrencyId = currency.CurrencyId,
				BankReferenceId = representer.BankResponeId,
				CreatedTimestamp = DateTimeOffset.Now,
				ModifiedTimestamp = DateTimeOffset.Now
			};
		}

		/// <summary>
		/// Map transaction and card detail for audit requests
		/// </summary>
		/// <param name="card"></param>
		/// <param name="transaction"></param>
		/// <param name="stringEncryptor"></param>
		/// <returns></returns>
		public static TransactionAuditRequest MapToTransactionAuditRequest(this BankCard card, Transaction transaction, IStringEncryptor stringEncryptor)
		{
			return new TransactionAuditRequest()
			{
				BankCardId = card.BankCardId,
				CardHolderName = card.CardHolderName,
				CardNumber = stringEncryptor.Decrypt(card.CardNumber),
				CVV = card.CVV,
				ExpiryMonth = card.ExpiryMonth,
				ExpiryYear = card.ExpiryYear,
				BankResponseId = transaction.BankReferenceId,
				Amount = transaction.Amount,
				TransactionStatus = transaction.TransactionStatus,
				ModifiedTimestamp = transaction.ModifiedTimestamp,
				CreatedTimestamp = transaction.CreatedTimestamp,

			};

		}
	}
}

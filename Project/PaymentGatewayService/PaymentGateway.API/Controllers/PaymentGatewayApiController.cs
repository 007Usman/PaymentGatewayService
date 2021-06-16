
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PaymentGateway.API.Helpers;
using PaymentGatway.Core.Encryption;
using PaymentGatway.Core.Models;
using PaymentGatway.Infrastructure.Repositories.Base;


namespace PaymentGateway.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PaymentGatewayApiController : ControllerBase
	{
		#region Properties
		private readonly IRepository<BankCard> _bankCardRepository;
		private readonly IRepository<Bank> _bankRepository;
		private readonly IRepository<Merchant> _merchantRepository;
		private readonly IRepository<Currency> _currencyRepository;
		private readonly IRepository<Transaction> _transactionRepository;
		private readonly IClientRequestService _clientRequest;
		private IStringEncryptor _stringEncryptor;
		private readonly ILogger<PaymentGatewayApiController> _logger;

		#endregion

		#region Constructor

		public PaymentGatewayApiController(
			IRepository<BankCard> bankCardRepository,
			IRepository<Bank> bankRepository,
			IRepository<Merchant> merchantRepository,
			IRepository<Currency> currencyRepository,
			IRepository<Transaction> transactionRepository,
			IClientRequestService clientRequest,
			IStringEncryptor stringEncryptor,
			ILogger<PaymentGatewayApiController> logger
			)
		{
			_bankCardRepository = bankCardRepository;
			_bankRepository = bankRepository;
			_merchantRepository = merchantRepository;
			_currencyRepository = currencyRepository;
			_transactionRepository = transactionRepository;
			_clientRequest = clientRequest;
			_stringEncryptor = stringEncryptor;
			_logger = logger;
		}


		#endregion


		/// <summary>
		/// Recevice merchant request and submit to the bank
		/// Encrypted Card number cannot be compared against merchant request  
		/// </summary>
		/// <param name="merchantRequest"></param>
		/// <returns></returns>
		[HttpPost]
		[Route("ProcessTransactions")]
		public async Task<IActionResult> ProcessTransactions(MerchantRequest merchantRequest)
		{
			_logger.LogInformation("Validation Successful! Extracting information from database ");
			var bank = _bankRepository.GetEntity(s => s.BankName == merchantRequest.BankName);

			var merchant = _merchantRepository.GetEntity(s => s.MerchantName == merchantRequest.MerchantName);

			var currency = _currencyRepository.GetEntity(s => s.CurrencyCode == merchantRequest.CurrencyCode);

			var card = _bankCardRepository.GetEntity(s =>
				s.CardHolderName == merchantRequest.CardHolderName &&
				s.ExpiryMonth == merchantRequest.ExpiryMonth &&
				s.ExpiryYear == merchantRequest.ExpiryYear &&
				s.CVV == merchantRequest.CVV);

			var cardDetail = merchantRequest.MapToBankCard(card, _stringEncryptor);
			if (card == null)
			{
				_logger.LogInformation("Adding Card details");
				await _bankCardRepository.AddAsync(cardDetail);
			}

			_logger.LogInformation($"Sending payment details to the {nameof(Bank)}");
			var transaction = await _clientRequest.SubmitTransaction(merchantRequest);
			if (transaction.BankResponeId == Guid.Empty)
				return BadRequest($"{nameof(Bank)} unable to provide Reference number");

			_logger.LogInformation($"Received {transaction.TransactionStatus} {nameof(Transaction)} response from bank");
			var transactionDetail = merchantRequest.MapToTransaction(transaction, merchant, bank, cardDetail, currency);

			_logger.LogInformation($"Adding {nameof(Transaction)} details to the database");
			return Ok(await _transactionRepository.AddAsync(transactionDetail));
		}


		/// <summary>
		/// Find transaction by transactionId to enable Reconcilation process
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpGet]
		[Route("TransactionId/{id}")]
		public async Task<IActionResult> GetTransactionById(Guid id)
		{
			_logger.LogInformation($"Checking {nameof(Transaction)} in the database against: {id}");

			var transactionId = await _transactionRepository.GetByIdAsync(id);
			if (transactionId is null)
				return NotFound("No Record found");

			var card = await _bankCardRepository.GetByIdAsync(transactionId.BankCardId);

			var response = card.MapToTransactionAuditRequest(transactionId, _stringEncryptor);
			response.CardNumber = BankCardHelper.MaskCardNumber(response.CardNumber);

			_logger.LogInformation("A match found, displaying the result");
			return Ok(response);
		}

		/// <summary>
		/// Add Merchant details
		/// </summary>
		/// <param name="merchant"></param>
		/// <returns></returns>
		[HttpPost]
		[Route("AddMerchant")]
		public async Task<IActionResult> AddMerchant(Merchant merchant)
		{
			_logger.LogInformation($"Validating {nameof(Merchant)}");
			if (merchant is null)
				return BadRequest($"Please provide valid {nameof(Merchant)} detials");

			var lookUpMerchant = _merchantRepository.GetEntity(s => s.MerchantName == merchant.MerchantName);
			if (lookUpMerchant != null)
				return Ok();

			var merchantDetails = await _merchantRepository.AddAsync(merchant);
			_logger.LogInformation($"{nameof(Merchant)} added to the database");

			return Ok(merchantDetails);
		}

		/// <summary>
		/// Add Currencies
		/// </summary>
		/// <param name="currency"></param>
		/// <returns></returns>
		[HttpPost]
		[Route("AddCurrency")]
		public async Task<IActionResult> AddCurrency(Currency currency)
		{
			_logger.LogInformation($"Validating {nameof(Currency)}");
			if (currency is null)
				return BadRequest($"Please provide valid {nameof(Currency)} detials");

			var lookUpCurrency = _currencyRepository.GetEntity(s => s.CurrencyCode == currency.CurrencyCode);
			if (lookUpCurrency != null)
				return Ok();

			var currencyDetail = await _currencyRepository.AddAsync(currency);
			_logger.LogInformation($"{nameof(Currency)} added to the database");

			return Ok(currencyDetail);
		}

		/// <summary>
		/// Add Bank
		/// </summary>
		/// <param name="bank"></param>
		/// <returns></returns>
		[HttpPost]
		[Route("AddBank")]
		public async Task<IActionResult> AddBank(Bank bank)
		{
			_logger.LogInformation($"Validating Merchant");
			if (bank is null)
				return BadRequest($"Please provide valid {nameof(Bank)} detials");

			var lookUpBank = _bankRepository.GetEntity(s => s.BankName == bank.BankName);
			if (lookUpBank != null)
				return Ok();

			var bankDetail = await _bankRepository.AddAsync(bank);
			_logger.LogInformation($"{nameof(Currency)} added to the database");

			return Ok(bankDetail);
		}
	}
}

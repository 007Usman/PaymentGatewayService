using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PaymentGatway.Core.Models;

namespace Banking.Api.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class TransactionController : ControllerBase
	{
		public readonly ILogger<TransactionController> _logger;
		private readonly IMerchantResponseRepresenter _responseRepresenter;

		public TransactionController(ILogger<TransactionController> logger, IMerchantResponseRepresenter responseRepresenter)
		{
			_logger = logger;
			_responseRepresenter = responseRepresenter;
		}


		[HttpPost]
		[Route("Bank", Name = "ProcessTransaction")]
		public IActionResult ProcessTransaction([FromBody] BankTransactionRegister transaction)
		{
			_logger.LogInformation("Request validated, preparing reponse");
			if (transaction.TransactionAmount < 5 || transaction.TransactionAmount > 10000)
			{
				_responseRepresenter.BankResponseId = Guid.NewGuid();
				_responseRepresenter.TransactionStatus = TransactionStatus.Unsuccessful;
			}
			else
			{
				_responseRepresenter.BankResponseId = Guid.NewGuid();
				_responseRepresenter.TransactionStatus = TransactionStatus.Successful;
			}


			return Ok(_responseRepresenter);
		}

	}
}
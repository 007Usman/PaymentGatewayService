using System;

namespace PaymentGatway.Core.Models
{
	public interface IBank : IEntityTimestamp
	{
		public Guid BankId { get; set; }
		public string BankName { get; set; }
	}
}

using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PaymentGatway.Core.Models
{
	public class Bank : IBank
	{
		[Key]
		[JsonIgnore]
		public Guid BankId { get; set; }
		public string BankName { get; set; }

		[JsonIgnore]
		public DateTimeOffset CreatedTimestamp { get; set; }

		[JsonIgnore]
		public DateTimeOffset ModifiedTimestamp { get; set; }
	}


}



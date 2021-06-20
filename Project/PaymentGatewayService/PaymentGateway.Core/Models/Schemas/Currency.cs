using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PaymentGatway.Core.Models
{
	public class Currency : ICurrency
	{
		[Key]
		[JsonIgnore]
		public Guid CurrencyId { get; set; }

		public string CurrencyCode { get; set; }
		public string CurrencyDescription { get; set; }

		[JsonIgnore]
		public DateTimeOffset CreatedTimestamp { get; set; }

		[JsonIgnore]
		public DateTimeOffset ModifiedTimestamp { get; set; }
	}


}



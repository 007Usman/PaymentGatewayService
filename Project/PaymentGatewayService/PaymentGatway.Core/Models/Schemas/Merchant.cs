using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PaymentGatway.Core.Models
{
	public class Merchant : IMerchant
	{
		[Key]
		[JsonIgnore]
		public Guid MerchantId { get; set; }

		public string MerchantName { get; set; }
		public string Description { get; set; }

		[JsonIgnore]
		public DateTimeOffset CreatedTimestamp { get; set; }

		[JsonIgnore]
		public DateTimeOffset ModifiedTimestamp { get; set; }
	}


}



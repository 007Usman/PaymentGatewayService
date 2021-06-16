using System.Threading.Tasks;
using PaymentGatway.Core.Models;

namespace PaymentGateway.API.Helpers
{
	public interface IClientRequestService
	{
		Task<BankResponseRepresentor> SubmitTransaction(MerchantRequest request);
	}
}

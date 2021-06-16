using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PaymentGatway.Core.Models;

namespace PaymentGateway.API.Helpers
{

	public class ClientRequestService : IClientRequestService
	{
		private readonly HttpClient _httpCleint;

		public ClientRequestService(HttpClient httpCleint)
		{
			_httpCleint = httpCleint;
		}

		/// <summary>
		/// Send transaction to bank and receive response
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		public async Task<BankResponseRepresentor> SubmitTransaction(MerchantRequest request)
		{
			var message = JsonConvert.SerializeObject(request.MapToPaymentRepresentor());
			var data = new StringContent(message, Encoding.UTF8, ClientRequest.MediaType);

			var response = await _httpCleint.PostAsync(ClientRequest.ActionMethod, data);
			string result = await response.Content.ReadAsStringAsync();

			return result.MapToBankResponseRepresentor();
		}
	}
}

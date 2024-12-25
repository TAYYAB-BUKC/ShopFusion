using Newtonsoft.Json;
using ShopFusion.Client.Services.Interfaces;
using ShopFusion.Models.DTOs;
using ShopFusion.Models.Entities;
using System.Net.Http;
using System.Text;

namespace ShopFusion.Client.Services
{
	public class PaymentService : IPaymentService
	{
		private readonly HttpClient _httpClient;
		private readonly string _apiBaseURL;

		public PaymentService(HttpClient httpClient, IConfiguration configuration)
		{
			_httpClient = httpClient;
			_apiBaseURL = configuration.GetSection("API_BASEURL").Value;
		}

		public async Task<SuccessModelDTO> ProcessPayment(StripePaymentDTO stripePayment)
		{
			try
			{
				var content = JsonConvert.SerializeObject(stripePayment);
				var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
				var response = await _httpClient.PostAsync($"{_apiBaseURL}/stripepayment/processpayment", bodyContent);
				var responseContent = await response.Content.ReadAsStringAsync();

				if (response.IsSuccessStatusCode)
				{
					var result = JsonConvert.DeserializeObject<SuccessModelDTO>(responseContent);
					return result;
				}
				else
				{
					var result = JsonConvert.DeserializeObject<ErrorModelDTO>(responseContent);
					throw new Exception(result.Message);
				}
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
	}
}
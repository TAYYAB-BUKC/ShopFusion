using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using ShopFusion.Client.Services.Interfaces;
using ShopFusion.Common;
using ShopFusion.Models.DTOs;
using System.Net.Http.Headers;
using System.Text;

namespace ShopFusion.Client.Services
{
	public class OrderService : IOrderService
	{
		private readonly HttpClient _httpClient;
		private readonly string _apiBaseURL;

		public OrderService(HttpClient httpClient, IConfiguration configuration)
		{
			_httpClient = httpClient;
			_apiBaseURL = configuration.GetSection("API_BASEURL").Value;
		}

		public async Task<CustomOrderDTO> CreateOrder(StripePaymentDTO order)
		{
			var content = JsonConvert.SerializeObject(order);
			var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
			var response = await _httpClient.PostAsync($"{_apiBaseURL}/order/create", bodyContent);
			if (response.IsSuccessStatusCode)
			{
				var responseContent = await response.Content.ReadAsStringAsync();
				var result = JsonConvert.DeserializeObject<CustomOrderDTO>(responseContent);
				return result;
			}

			return new CustomOrderDTO();
		}

		public async Task<IEnumerable<CustomOrderDTO>> GetAllOrders()
		{
			var orders = new List<CustomOrderDTO>();
			var response = await _httpClient.GetAsync($"{_apiBaseURL}/Order/GetAll");
			if (response.IsSuccessStatusCode)
			{
				var content = await response.Content.ReadAsStringAsync();
				orders = JsonConvert.DeserializeObject<List<CustomOrderDTO>>(content);
			}
			return orders;
		}

		public async Task<CustomOrderDTO> GetOrderById(int orderId)
		{
			var order = new CustomOrderDTO();
			var response = await _httpClient.GetAsync($"{_apiBaseURL}/Order/GetById/{orderId}");
			if (response.IsSuccessStatusCode)
			{
				var content = await response.Content.ReadAsStringAsync();
				order = JsonConvert.DeserializeObject<CustomOrderDTO>(content);
			}

			return order;
		}
	}
}
using Newtonsoft.Json;
using ShopFusion.Client.Services.Interfaces;
using ShopFusion.Models.DTOs;

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
using Newtonsoft.Json;
using ShopFusion.Client.Services.Interfaces;
using ShopFusion.Models.DTOs;
using System.Text.Json.Serialization;

namespace ShopFusion.Client.Services
{
	public class ProductService : IProductService
	{
		private readonly HttpClient _httpClient;
		public ProductService(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task<IEnumerable<ProductDTO>> GetAllProducts()
		{
			var products = new List<ProductDTO>();
			var response = await _httpClient.GetAsync("/product");
			if (response.IsSuccessStatusCode) 
			{
				var content = await response.Content.ReadAsStringAsync();
				products = JsonConvert.DeserializeObject<List<ProductDTO>>(content);
			}

			return products;
		}

		public async Task<ProductDTO> GetProductById(int productId)
		{
			var product = new ProductDTO();
			var response = await _httpClient.GetAsync($"/product/{productId}");
			if (response.IsSuccessStatusCode)
			{
				var content = await response.Content.ReadAsStringAsync();
				product = JsonConvert.DeserializeObject<ProductDTO>(content);
			}

			return product;
		}
	}
}

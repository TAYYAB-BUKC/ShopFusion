using Newtonsoft.Json;
using ShopFusion.Client.Services.Interfaces;
using ShopFusion.Models.DTOs;
using System.Text.Json.Serialization;

namespace ShopFusion.Client.Services
{
	public class ProductService : IProductService
	{
		private readonly HttpClient _httpClient;
		private readonly string _serverBaseURL;
		public ProductService(HttpClient httpClient, IConfiguration configuration)
		{
			_httpClient = httpClient;
			_serverBaseURL = configuration.GetSection("SERVER_BASEURL").Value;
		}

		public async Task<IEnumerable<ProductDTO>> GetAllProducts()
		{
			var products = new List<ProductDTO>();
			var response = await _httpClient.GetAsync($"{_httpClient.BaseAddress}/product");
			//var response = await _httpClient.GetAsync("/product");
			if (response.IsSuccessStatusCode) 
			{
				var content = await response.Content.ReadAsStringAsync();
				products = JsonConvert.DeserializeObject<List<ProductDTO>>(content);
				products.ForEach(p => p.ImageURL = $"{_serverBaseURL}{p.ImageURL}");
			}
			return products;
		}

		public async Task<ProductDTO> GetProductById(int productId)
		{
			var product = new ProductDTO();
			var response = await _httpClient.GetAsync($"{_httpClient.BaseAddress}/product/{productId}");
			if (response.IsSuccessStatusCode)
			{
				var content = await response.Content.ReadAsStringAsync();
				product = JsonConvert.DeserializeObject<ProductDTO>(content);
				product.ImageURL = $"{_serverBaseURL}{product.ImageURL}";
			}

			return product;
		}
	}
}

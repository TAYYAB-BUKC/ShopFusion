using ShopFusion.Models.DTOs;

namespace ShopFusion.Client.Services.Interfaces
{
	public interface IProductService
	{
		public Task<IEnumerable<ProductDTO>> GetAllProducts();
		public Task<ProductDTO> GetProductById(int productId);
	}
}
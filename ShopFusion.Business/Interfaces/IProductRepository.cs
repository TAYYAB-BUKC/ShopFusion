using ShopFusion.Models.DTOs;

namespace ShopFusion.Business.Interfaces
{
	public interface IProductRepository
	{
		public Task<List<ProductDTO>> GetAll();
		public Task<ProductDTO> GetById(int id);
		public Task<ProductDTO> Create(ProductDTO product);
		public Task<ProductDTO> Update(ProductDTO product);
		public Task<int> Delete(int id);
	}
}
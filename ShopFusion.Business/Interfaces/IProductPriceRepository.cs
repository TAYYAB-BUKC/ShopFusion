using ShopFusion.Models.DTOs;

namespace ShopFusion.Business.Interfaces
{
	public interface IProductPriceRepository
	{
		public Task<List<ProductPricesDTO>> GetAll(int? productID = null);
		public Task<ProductPricesDTO> GetById(int id);
		public Task<ProductPricesDTO> Create(ProductPricesDTO productPricesDTO);
		public Task<ProductPricesDTO> Update(ProductPricesDTO productPricesDTO);
		public Task<int> Delete(int id);
	}
}
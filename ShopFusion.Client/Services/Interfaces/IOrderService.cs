using ShopFusion.Models.DTOs;

namespace ShopFusion.Client.Services.Interfaces
{
	public interface IOrderService
	{
		public Task<IEnumerable<CustomOrderDTO>> GetAllOrders();
		public Task<CustomOrderDTO> GetOrderById(int orderId);
	}
}
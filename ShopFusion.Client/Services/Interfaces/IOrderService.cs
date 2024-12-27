using ShopFusion.Models.DTOs;

namespace ShopFusion.Client.Services.Interfaces
{
	public interface IOrderService
	{
		public Task<IEnumerable<CustomOrderDTO>> GetAllOrders();
		public Task<CustomOrderDTO> GetOrderById(int orderId);
		public Task<CustomOrderDTO> CreateOrder(StripePaymentDTO order);
		public Task<OrderDTO> MarkPaymentSuccessful(OrderDTO order);
	}
}
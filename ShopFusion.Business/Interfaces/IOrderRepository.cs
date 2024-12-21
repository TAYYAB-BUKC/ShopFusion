using ShopFusion.Models.DTOs;

namespace ShopFusion.Business.Interfaces
{
	public interface IOrderRepository
	{
		public Task<List<CustomOrderDTO>> GetAll(string? userId, string? status);
		public Task<CustomOrderDTO> GetById(int orderId);
		public Task<CustomOrderDTO> Create(CustomOrderDTO order);
		public Task<int> DeleteById(int orderId);
		public Task<List<OrderDTO>> Update(OrderDTO order);
		public Task<OrderDTO> MarkPaymentSuccessful(int id);
		public Task<List<bool>> UpdateStatus(int orderId, string status);
	}
}
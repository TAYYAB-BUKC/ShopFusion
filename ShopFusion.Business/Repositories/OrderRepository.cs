using AutoMapper;
using ShopFusion.Business.Interfaces;
using ShopFusion.DataAccess.Data;
using ShopFusion.Models.DTOs;
using ShopFusion.Models.Entities;

namespace ShopFusion.Business.Repositories
{
	public class OrderRepository : IOrderRepository
	{
		private readonly ApplicationDbContext _dbContext;
		private readonly IMapper _mapper;

		public OrderRepository(ApplicationDbContext dbContext, IMapper mapper)
		{
			_dbContext = dbContext;
			_mapper = mapper;
		}

		public async Task<CustomOrderDTO> Create(CustomOrderDTO orderDTO)
		{
			var order = _mapper.Map<CustomOrderDTO, CustomOrder>(orderDTO);
			await _dbContext.Order.AddAsync(order.Order);
			await _dbContext.SaveChangesAsync();

			order.OrderDetails.ForEach(od => od.OrderId = order.Order.Id);

			await _dbContext.OrderDetails.AddRangeAsync(order.OrderDetails);
			await _dbContext.SaveChangesAsync();


			return new CustomOrderDTO
			{
				Order = _mapper.Map<Order, OrderDTO>(order.Order),
				OrderDetails = _mapper.Map<List<OrderDetails>, List<OrderDetailsDTO>>(order.OrderDetails),
			};
		}


		public Task<int> DeleteById(int orderId)
		{
			throw new NotImplementedException();
		}

		public Task<List<CustomOrderDTO>> GetAll(string? userId, string? status)
		{
			throw new NotImplementedException();
		}

		public Task<CustomOrderDTO> GetById(int orderId)
		{
			throw new NotImplementedException();
		}

		public Task<List<OrderDTO>> MarkPaymentSuccessful(int id)
		{
			throw new NotImplementedException();
		}

		public Task<List<OrderDTO>> Update(OrderDTO order)
		{
			throw new NotImplementedException();
		}

		public Task<List<bool>> UpdateStatus(int orderId, string status)
		{
			throw new NotImplementedException();
		}
	}
}
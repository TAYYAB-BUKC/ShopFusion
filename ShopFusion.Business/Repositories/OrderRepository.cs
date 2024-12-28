using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShopFusion.Business.Interfaces;
using ShopFusion.Common;
using ShopFusion.DataAccess.Data;
using ShopFusion.Models.DTOs;
using ShopFusion.Models.Entities;
using System.Threading;

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
			try
			{
				var order = _mapper.Map<CustomOrderDTO, CustomOrder>(orderDTO);
				order.Order.OrderDate = DateTime.Now;
				await _dbContext.Orders.AddAsync(order.Order);
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
			catch (Exception ex)
			{
				throw;
			}
		}


		public async Task<int> DeleteById(int orderId)
		{
			var order = await _dbContext.Orders.FirstOrDefaultAsync(o => o.Id == orderId);
			if(order != default(Order))
			{
				var orderDetails = _dbContext.OrderDetails.Where(od => od.OrderId == orderId);
				_dbContext.OrderDetails.RemoveRange(orderDetails);
				_dbContext.Orders.Remove(order);
				return await _dbContext.SaveChangesAsync();
			}
			return 0;
		}

		public async Task<List<CustomOrderDTO>> GetAll(string? userId, string? status)
		{
			List<CustomOrder> allOrders = new List<CustomOrder>();
			List<Order> orders = await _dbContext.Orders.ToListAsync();
			List<OrderDetails> orderDtails = await _dbContext.OrderDetails.ToListAsync();

			foreach (var order in orders)
			{
				allOrders.Add(new CustomOrder()
				{
					Order = order,
					OrderDetails = orderDtails.Where(od => od.OrderId == order.Id).ToList()
				});
			}

			//Filter orders based on userid and status #TODO

			return _mapper.Map<List<CustomOrder>, List<CustomOrderDTO>>(allOrders);
		}

		public async Task<CustomOrderDTO> GetById(int orderId)
		{
			var order = await _dbContext.Orders.FirstOrDefaultAsync(o => o.Id == orderId);
			if (order != default(Order))
			{
				var orderDetails = _dbContext.OrderDetails.Where(od => od.OrderId == orderId).ToList();
				return new CustomOrderDTO
				{
					Order = _mapper.Map<Order, OrderDTO>(order),
					OrderDetails = _mapper.Map<List<OrderDetails>, List<OrderDetailsDTO>>(orderDetails),
				};
			}
			return new CustomOrderDTO();
		}

		public async Task<OrderDTO> MarkPaymentSuccessful(int orderId)
		{
			var order = await _dbContext.Orders.FirstOrDefaultAsync(o => o.Id == orderId);
			if (order == default(Order) || order.Status != CommonConfiguration.Status_Pending)
			{
				return new OrderDTO();
			}

			order.Status = CommonConfiguration.Status_Confirmed;
			_dbContext.Orders.Update(order);
			await _dbContext.SaveChangesAsync();

			return _mapper.Map<Order, OrderDTO>(order);
		}

		public async Task<OrderDTO> Update(OrderDTO orderDTO)
		{
			if(orderDTO != null)
			{
				var orderFromDB = await _dbContext.Orders.FindAsync(orderDTO.Id);
				orderFromDB.Name = orderDTO.Name;
				orderFromDB.PhoneNumber = orderDTO.PhoneNumber;
				orderFromDB.StreetAddress = orderDTO.StreetAddress;
				orderFromDB.City = orderDTO.City;
				orderFromDB.State = orderDTO.State;
				orderFromDB.PostalCode = orderDTO.PostalCode;
				orderFromDB.Tracking = orderDTO.Tracking;
				orderFromDB.Carrier = orderDTO.Carrier;
				orderFromDB.Status = orderDTO.Status;

				if (orderFromDB.Status == CommonConfiguration.Status_Shipped)
				{
					orderFromDB.ShippingDate = DateTime.Now;
				}

				await _dbContext.SaveChangesAsync();
				return _mapper.Map<Order, OrderDTO>(orderFromDB);
			}
			return new OrderDTO();
		}

		public async Task<bool> UpdateStatus(int orderId, string status)
		{
			var order = await _dbContext.Orders.FirstOrDefaultAsync(o => o.Id == orderId);
			if (order == default(Order))
			{
				return false;
			}

			order.Status = status;
			if (order.Status == CommonConfiguration.Status_Shipped)
			{
				order.ShippingDate = DateTime.Now;
			}
			_dbContext.Orders.Update(order);
			await _dbContext.SaveChangesAsync();
			return true;
		}
	}
}
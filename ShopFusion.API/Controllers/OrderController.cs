using Microsoft.AspNetCore.Mvc;
using ShopFusion.Business.Interfaces;
using ShopFusion.Models.DTOs;
using Stripe.Checkout;

namespace ShopFusion.API.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class OrderController : ControllerBase
	{
		public IOrderRepository _orderRepository { get; set; }
		public OrderController(IOrderRepository orderRepository)
		{
			_orderRepository = orderRepository;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			return Ok(await _orderRepository.GetAll(null, null));
		}

		[HttpGet("{orderId}")]
		public async Task<IActionResult> GetById(int? orderId)
		{
			if (orderId == null || orderId == 0)
			{
				return BadRequest(new ErrorModelDTO()
				{
					Message = "Please specify order id",
					StatusCode = StatusCodes.Status400BadRequest
				});
			}

			var order = await _orderRepository.GetById(orderId.Value);
			if (order.Order.Id <= 0)
			{
				return BadRequest(new ErrorModelDTO()
				{
					Message = "Order not found",
					StatusCode = StatusCodes.Status404NotFound
				});
			}

			return Ok(order);
		}

		[HttpPost]
		[ActionName("Create")]
		public async Task<IActionResult> CreateOrder([FromBody] StripePaymentDTO model)
		{
			var response = await _orderRepository.Create(model.Order);
			return Ok(response);
		}

		[HttpPost]
		[ActionName("PaymentSuccessful")]
		public async Task<IActionResult> MarkPaymentSuccessful([FromBody] OrderDTO order)
		{
			var session = new SessionService();
			var orderSessionDetails = await session.GetAsync(order.SessionId);
			if(orderSessionDetails.PaymentStatus == "paid")
			{
				var response = await _orderRepository.MarkPaymentSuccessful(order.Id, orderSessionDetails.PaymentIntentId);
				if(response == default(OrderDTO))
				{
					return BadRequest(new ErrorModelDTO()
					{
						Message = "Unable to mark payment successful."
					});
				}
				return Ok(response);
			}

			return BadRequest(new ErrorModelDTO()
			{
				Message = "Something went wrong while marking payment successful."
			});
		}
	}
}
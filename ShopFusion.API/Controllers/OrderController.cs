﻿using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using ShopFusion.API.HelperClasses;
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
		public IEmailSender _emailSender { get; set; }
		public OrderController(IOrderRepository orderRepository, IEmailSender emailSender)
		{
			_orderRepository = orderRepository;
			_emailSender = emailSender;
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
				await _emailSender.SendEmailAsync(order.Email, "Order Confirmation - Shop Fusion",
					$"Dear {order.Name}," +
					$"\r\n\r\nThank you for your order!" +
					$"\r\n\r\nOrder Number: {order.Id}" +
					$"\r\nOrder Date: {order.OrderDate}" +
					$"\r\nEstimated Delivery: In Next 7-10 Business Days" +
					$"\r\nTotal Amount: {order.GrandTotal}" +
					$"\r\n\r\nWe’ll notify you once your order ships. If you have any questions, feel free to reach out at support@shopfusion.com" +
					$"\r\n\r\nThanks for choosing Shop Fusion!" +
					$"\r\n\r\nBest regards," +
					$"\r\nTayyab Arsalan" +
					$"\r\nShop Fusion"
					);
				return Ok(response);
			}

			return BadRequest(new ErrorModelDTO()
			{
				Message = "Something went wrong while marking payment successful."
			});
		}
	}
}
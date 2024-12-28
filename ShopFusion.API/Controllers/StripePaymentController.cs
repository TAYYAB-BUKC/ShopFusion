using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopFusion.Models.DTOs;
using Stripe.Checkout;

namespace ShopFusion.API.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class StripePaymentController : ControllerBase
	{
		private readonly string _clientBaseURL;

		public StripePaymentController(IConfiguration configuration)
		{
			_clientBaseURL = configuration.GetSection("CLIENT_BASEURL").Value;
		}

		[HttpPost]
		[Authorize]
		public IActionResult ProcessPayment([FromBody] StripePaymentDTO model)
		{
			try
			{
				var sessionOptions = new SessionCreateOptions
				{
					SuccessUrl = $"{_clientBaseURL}{model.SuccessURL}",
					CancelUrl = $"{_clientBaseURL}{model.CancelURL}",
					LineItems = new List<SessionLineItemOptions>(),
					Mode = "payment",
					PaymentMethodTypes = new List<string>() { "card" }
				};

				foreach (var orderItem in model.Order.OrderDetails)
				{
					var sessionLineItemOptions = new SessionLineItemOptions()
					{
						PriceData = new SessionLineItemPriceDataOptions()
						{
							Currency = "USD",
							UnitAmountDecimal = (decimal)orderItem.Price * 100,
							ProductData = new SessionLineItemPriceDataProductDataOptions()
							{
								Name = orderItem.ProductName,
								Images = new List<string> { orderItem.Product.ImageURL },
								Description = orderItem.Product.Description,
							}
						},
						Quantity = orderItem.Count
					};

					sessionOptions.LineItems.Add(sessionLineItemOptions);
				}

				var service = new SessionService();
				var session = service.Create(sessionOptions);

				return Ok(new SuccessModelDTO()
				{
					Data = $"{session.Id}^;{session.PaymentIntentId}",
				});
			}
			catch (Exception ex)
			{
				return BadRequest(new ErrorModelDTO()
				{
					Message = ex.Message,					
				});
			}
		}
	}
}
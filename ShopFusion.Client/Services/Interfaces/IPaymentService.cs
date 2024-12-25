using ShopFusion.Models.DTOs;

namespace ShopFusion.Client.Services.Interfaces
{
	public interface IPaymentService
	{
		Task<SuccessModelDTO> ProcessPayment(StripePaymentDTO stripePayment);
	}
}
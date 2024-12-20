using ShopFusion.Models.ViewModels;

namespace ShopFusion.Client.Services.Interfaces
{
	public interface ICartService
	{
		public event Action OnChange;
		public Task DecrementCart(CartViewModel cartViewModel);
		public Task IncrementCart(CartViewModel cartViewModel);
	}
}
using Blazored.LocalStorage;
using ShopFusion.Client.Services.Interfaces;
using ShopFusion.Common;
using ShopFusion.Models.ViewModels;

namespace ShopFusion.Client.Services
{
	public class CartService : ICartService
	{
		public event Action OnChange;
		public readonly ILocalStorageService _localStorageService;

		public CartService(ILocalStorageService localStorageService)
		{
			_localStorageService = localStorageService;
		}

		public async Task DecrementCart(CartViewModel cartViewModel)
		{
			var cartList = await _localStorageService.GetItemAsync<List<CartViewModel>>(CommonConfiguration.CartKey);

			for (int i = 0; i < cartList.Count; i++)
			{
				if (cartList[i].ProductId == cartViewModel.ProductId && cartList[i].ProductPriceId == cartViewModel.ProductPriceId)
				{
					if (cartList[i].Count == 1 || (cartList[i].Count - cartViewModel.Count) <= 0)
					{
						cartList.Remove(cartList[i]);
					}
					else
					{
						cartList[i].Count -= cartViewModel.Count;
					}
				}
			}

			await _localStorageService.SetItemAsync(CommonConfiguration.CartKey, cartList);
			OnChange.Invoke();
		}

		public async Task IncrementCart(CartViewModel cartViewModel)
		{
			var cartList = await _localStorageService.GetItemAsync<List<CartViewModel>>(CommonConfiguration.CartKey);
			bool IsAlreadyInCart = false;
			if(cartList == null)
			{
				cartList = new List<CartViewModel>();
			}

			foreach (var cartItem in cartList)
			{
				if(cartItem.ProductId == cartViewModel.ProductId && cartItem.ProductPriceId == cartViewModel.ProductPriceId)
				{
					IsAlreadyInCart = true;
					cartItem.Count += cartViewModel.Count;
				}
			}

			if (!IsAlreadyInCart)
			{
				cartList.Add(new CartViewModel()
				{
					Count = cartViewModel.Count,
					ProductId = cartViewModel.ProductId,
					ProductPriceId = cartViewModel.ProductPriceId,
				});
			}

			await _localStorageService.SetItemAsync(CommonConfiguration.CartKey, cartList);
			OnChange.Invoke();
		}
	}
}
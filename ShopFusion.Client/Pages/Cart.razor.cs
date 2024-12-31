using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using ShopFusion.Client.Services;
using ShopFusion.Client.Services.Interfaces;
using ShopFusion.Common;
using ShopFusion.Models.DTOs;
using ShopFusion.Models.ViewModels;
using System;

namespace ShopFusion.Client.Pages
{
	public partial class Cart
	{
		[Inject]
		public ILocalStorageService LocalStorageService {  get; set; }
		[Inject]
		public IProductService ProductService { get; set; }
		[Inject]
		public ICartService CartService { get; set; }

		public bool IsProcessing { get; set; } = true;
		public List<CartViewModel> CartItems { get; set; }
		public IEnumerable<ProductDTO> Products { get; set; }
		public double OrderTotal { get; set; } = 0;

		protected override async Task OnAfterRenderAsync(bool firstRender)
		{
			if (firstRender)
			{
				await LoadCart();
				IsProcessing = false;
				StateHasChanged();
			}
		}

		private async Task LoadCart()
		{
			OrderTotal = 0;
			CartItems = await LocalStorageService.GetItemAsync<List<CartViewModel>>(CommonConfiguration.CartKey);
			Products = await ProductService.GetAllProducts();
			foreach (var cartItem in CartItems)
			{
				cartItem.Product = Products.FirstOrDefault(p => p.Id == cartItem.ProductId);
				cartItem.ProductPrice = cartItem.Product.ProductPrices.FirstOrDefault(p => p.Id == cartItem.ProductPriceId);
				OrderTotal += (cartItem.ProductPrice.Price * cartItem.Count);
			}
		}

		public async Task IncrementCart(CartViewModel model)
		{
			IsProcessing = true;
			model.Count = 1;
			await CartService.IncrementCart(model);
			await LoadCart();
			IsProcessing = false;
		}

		public async Task DecrementCart(CartViewModel model)
		{
			IsProcessing = true;
			model.Count = 1;
			await CartService.DecrementCart(model);
			await LoadCart();
			IsProcessing = false;
		}

		public async Task RemoveItemFromCart(CartViewModel model)
		{
			IsProcessing = true;
			await CartService.DecrementCart(model);
			await LoadCart();
			IsProcessing = false;
		}
	}
}
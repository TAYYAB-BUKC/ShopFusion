using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using ShopFusion.Client.HelperClasses;
using ShopFusion.Client.Services.Interfaces;
using ShopFusion.Models.DTOs;
using ShopFusion.Models.ViewModels;
using System;

namespace ShopFusion.Client.Pages
{
	public partial class ProductDetails
	{
		[Inject]
		public IProductService ProductService { get; set; }
		[Inject]
		public ICartService CartService { get; set; }
		[Inject]
		public NavigationManager NavigationManager { get; set; }
		[Inject]
		public IJSRuntime JSRuntime { get; set; }

		[Parameter]
		public int ProductId { get; set; }
		public bool IsProcessing { get; set; }
		public ProductDTO product = new ProductDTO();
		ProductDetailsViewModel _productDetailsViewModel = new ProductDetailsViewModel();

		protected override async Task OnInitializedAsync()
		{
			IsProcessing = true;
			product = await ProductService.GetProductById(ProductId);
			IsProcessing = false;
		}

		private async Task ShowSelectedPrice(int id)
		{
			_productDetailsViewModel.ProductPrice = product.ProductPrices.FirstOrDefault(p => p.Id == id);
			if (_productDetailsViewModel.ProductPrice.Id > 0)
			{
				_productDetailsViewModel.SelectedProductPriceID = id;
			}
		}

		private async Task AddProductToCart()
		{
			CartViewModel model = new CartViewModel()
			{
				Count = _productDetailsViewModel.Count,
				ProductPriceId = _productDetailsViewModel.SelectedProductPriceID,
				ProductId = ProductId
			};

			await CartService.IncrementCart(model);
			NavigationManager.NavigateTo("/");
			await JSRuntime.ShowSuccessToastrNotification("Product added to cart successfully");
		}
	}
}
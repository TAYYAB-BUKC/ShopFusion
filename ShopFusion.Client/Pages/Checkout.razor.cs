using Blazored.LocalStorage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using ShopFusion.Client.HelperClasses;
using ShopFusion.Client.Services;
using ShopFusion.Client.Services.Interfaces;
using ShopFusion.Common;
using ShopFusion.Models.DTOs;
using ShopFusion.Models.ViewModels;
using System;

namespace ShopFusion.Client.Pages
{
	[Authorize]
	public partial class Checkout
	{
		[Inject]
		public ILocalStorageService LocalStorageService {  get; set; }
		[Inject]
		public IProductService ProductService { get; set; }
		[Inject]
		public IOrderService OrderService { get; set; }
		[Inject]
		public IJSRuntime JSRuntime { get; set; }
		[Inject]
		public IPaymentService PaymentService { get; set; }

		public bool IsProcessing { get; set; } = true;
		public CustomOrderDTO Order { get; set; }
		public IEnumerable<ProductDTO> Products { get; set; }

		protected override async Task OnAfterRenderAsync(bool firstRender)
		{
			if (firstRender)
			{
				var cartItems = await LocalStorageService.GetItemAsync<List<CartViewModel>>(CommonConfiguration.CartKey);
				Products = await ProductService.GetAllProducts();
				var userDetails = await LocalStorageService.GetItemAsync<UserDTO>(CommonConfiguration.UserDetails_Key);
				Order = new CustomOrderDTO()
				{
					Order = new OrderDTO()
					{
						UserId = userDetails?.Id,
						Name = userDetails?.Name,
						Email = userDetails?.Email,
						PhoneNumber = userDetails?.PhoneNumber,
						GrandTotal = 0,
						Status = CommonConfiguration.Status_Pending
					},
					OrderDetails = new List<OrderDetailsDTO>()
				};
				foreach (var cartItem in cartItems)
				{
					cartItem.Product = Products.FirstOrDefault(p => p.Id == cartItem.ProductId);
					cartItem.ProductPrice = cartItem.Product.ProductPrices.FirstOrDefault(p => p.Id == cartItem.ProductPriceId);
					Order.OrderDetails.Add(new OrderDetailsDTO()
					{
						Product = cartItem.Product,
						ProductId = cartItem.ProductId,
						ProductName = cartItem.Product.Name,
						Price = cartItem.ProductPrice.Price,
						Size = cartItem.ProductPrice.Size,
						Count = cartItem.Count,
					});
					Order.Order.GrandTotal += (cartItem.ProductPrice.Price * cartItem.Count);
				}
				IsProcessing = false;
				StateHasChanged();
			}
		}

		private async void PlaceOrder()
		{
			IsProcessing = true;
			try
			{
				StripePaymentDTO stripePaymentDTO = new StripePaymentDTO()
				{
					Order = Order
				};
				var paymentResponse = await PaymentService.ProcessPayment(stripePaymentDTO);
				stripePaymentDTO.Order.Order.SessionId = Convert.ToString(paymentResponse.Data);

				var result = await OrderService.CreateOrder(stripePaymentDTO);
				if (result == default(CustomOrderDTO))
				{
					await JSRuntime.ShowFailureToastrNotification("Something went wrong while creating your order. Please try again later.");
				}
				else
				{
					await LocalStorageService.SetItemAsync(CommonConfiguration.OrderDetails_Key, result);
					await JSRuntime.InvokeVoidAsync("RedirectUserToStripePayment", Convert.ToString(paymentResponse.Data));
				}
			}
			catch (Exception ex)
			{
				IsProcessing = false;
				await JSRuntime.ShowFailureToastrNotification(ex.Message);
			}
		}
	}
}
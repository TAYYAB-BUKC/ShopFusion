using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using ShopFusion.Client.Services;
using ShopFusion.Client.Services.Interfaces;
using ShopFusion.Common;
using ShopFusion.Models.DTOs;
using ShopFusion.Models.Entities;
using System;

namespace ShopFusion.Client.Pages
{
	public partial class OrderConfirmation
	{
		[Inject]
		public ILocalStorageService LocalStorageService { get; set; }
		[Inject]
		public IOrderService OrderService { get; set; }

		private bool IsProcessing { get; set; } = false;
		private CustomOrderDTO Order { get; set; }

		protected override async Task OnInitializedAsync()
		{
			IsProcessing = true;
			Order = await LocalStorageService.GetItemAsync<CustomOrderDTO>(CommonConfiguration.OrderDetails_Key);
			var updatedOrder = await OrderService.MarkPaymentSuccessful(Order.Order);
			if (updatedOrder.Status == CommonConfiguration.Status_Confirmed)
			{
				Order.Order.Status = updatedOrder.Status;
				await LocalStorageService.RemoveItemsAsync(new[] { CommonConfiguration.CartKey, CommonConfiguration.OrderDetails_Key });
			}
			IsProcessing = false;
		}
	}
}
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using ShopFusion.Common;
using ShopFusion.Models.DTOs;
using System;

namespace ShopFusion.Client.Pages
{
	public partial class OrderConfirmation
	{
		[Inject]
		public ILocalStorageService LocalStorageService { get; set; }
		private bool IsProcessing { get; set; } = false;
		private CustomOrderDTO Order { get; set; }

		protected override async Task OnInitializedAsync()
		{
			IsProcessing = true;
			Order = await LocalStorageService.GetItemAsync<CustomOrderDTO>(CommonConfiguration.OrderDetails_Key);
			IsProcessing = false;
		}
	}
}
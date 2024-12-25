using Microsoft.AspNetCore.Components;
using ShopFusion.Client.Services;
using ShopFusion.Client.Services.Interfaces;
using ShopFusion.Models.DTOs;

namespace ShopFusion.Client.Pages
{
	public partial class Login
	{
		[Inject]
		public IAuthenticationService AuthenticationService { get; set; }
		[Inject]
		public NavigationManager NavigationManager { get; set; }

		public SignInRequestDTO signInRequestDTO { get; set; } = new();
		public bool ShowError { get; set; } = false;
		public string ErrorMessage { get; set; }

		private async Task UserLogin()
		{
			ShowError = false;
			var response = await AuthenticationService.Login(signInRequestDTO);
			if (response.IsSuccessful)
			{
				NavigationManager.NavigateTo("/", forceLoad: true);
			}
			else
			{
				ShowError = true;
				ErrorMessage = response.ErrorMessage;
			}
		}
	}
}
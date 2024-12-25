using Microsoft.AspNetCore.Components;
using ShopFusion.Client.Services;
using ShopFusion.Client.Services.Interfaces;
using ShopFusion.Models.DTOs;
using System.Web;

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
				var absoluteURI = NavigationManager.ToAbsoluteUri(NavigationManager.Uri);
				var queryStrings = HttpUtility.ParseQueryString(absoluteURI.Query);
				var returnUrl = queryStrings["returnUrl"];
				
				if (String.IsNullOrWhiteSpace(returnUrl))
				{
					NavigationManager.NavigateTo("/");
				}
				else
				{
					NavigationManager.NavigateTo($"{NavigationManager.BaseUri}/{returnUrl}");
				}
			}
			else
			{
				ShowError = true;
				ErrorMessage = response.ErrorMessage;
			}
		}
	}
}
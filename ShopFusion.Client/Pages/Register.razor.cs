using Microsoft.AspNetCore.Components;
using ShopFusion.Client.Services.Interfaces;
using ShopFusion.Models.DTOs;
using System;

namespace ShopFusion.Client.Pages
{
	public partial class Register
	{
		[Inject]
		public IAuthenticationService _authenticationService { get; set; }
		[Inject]
		public NavigationManager _navigationManager { get; set; }

		public SignUpRequestDTO signUpRequestDTO { get; set; } = new();
		public bool ShowRegistrationErrors { get; set; } = false;
		public IEnumerable<string> Errors { get; set; }

		private async Task RegisterUser()
		{
			ShowRegistrationErrors = false;
			var response = await _authenticationService.Register(signUpRequestDTO);
			if (response.IsSuccessful)
			{
				_navigationManager.NavigateTo("/login");
			}
			else
			{
				ShowRegistrationErrors = true;
				Errors = response.Errors;
			}
		}
	}
}
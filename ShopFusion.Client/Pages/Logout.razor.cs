using Microsoft.AspNetCore.Components;
using ShopFusion.Client.Services.Interfaces;

namespace ShopFusion.Client.Pages
{
	public partial class Logout
	{
		[Inject]
		public IAuthenticationService AuthenticationService { get; set; }
		[Inject]
		public NavigationManager NavigationManager { get; set; }

		protected override async Task OnInitializedAsync()
		{
			await AuthenticationService.Logout();
			NavigationManager.NavigateTo("/", forceLoad: true);
		}
	}
}
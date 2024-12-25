using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace ShopFusion.Client.Pages
{
	public partial class RedirectUserToLogin
	{
		[CascadingParameter]
		public Task<AuthenticationState> AuthenticationState { get; set; }
		[Inject]
		public NavigationManager NavigationManager { get; set; }

		protected override async Task OnInitializedAsync()
		{
			var authenticationState = await AuthenticationState;

			if (authenticationState.User.Identity is null || !authenticationState.User.Identity.IsAuthenticated)
			{
				var absoluteURI = NavigationManager.ToBaseRelativePath(NavigationManager.Uri);
				if (String.IsNullOrWhiteSpace(absoluteURI))
				{
					NavigationManager.NavigateTo("/login");
				}
				else
				{
					NavigationManager.NavigateTo($"/login?returnUrl={absoluteURI}");
				}
			}
		}
	}
}
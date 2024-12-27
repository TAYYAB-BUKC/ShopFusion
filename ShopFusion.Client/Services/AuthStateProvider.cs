using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json.Linq;
using ShopFusion.Client.HelperClasses;
using ShopFusion.Common;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace ShopFusion.Client.Services
{
	public class AuthStateProvider : AuthenticationStateProvider
	{
		private readonly HttpClient _httpClient;
		public readonly ILocalStorageService _localStorageService;

		public AuthStateProvider(HttpClient httpClient, ILocalStorageService localStorageService)
		{
			_httpClient = httpClient;
			_localStorageService = localStorageService;
		}

		public override async Task<AuthenticationState> GetAuthenticationStateAsync()
		{
			var token = await _localStorageService.GetItemAsStringAsync(CommonConfiguration.JWTToken_Key);
			if(token == null)
			{
				return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
				//return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(new[]
				//{
				//	new Claim(ClaimTypes.Name, "ben@gmail.com")
				//}, "jwtAuthType")));
			}
			_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(JWTHelper.ParseClaimsFromJWT(token), "jwtAuthType")));
		}

		public void NotifyUserLoggedIn(string token)
		{
			var authState = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(JWTHelper.ParseClaimsFromJWT(token), "jwtAuthType")));
			NotifyAuthenticationStateChanged(Task.FromResult(authState));
		}

		public void NotifyUserLogout()
		{
			var authState = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
			NotifyAuthenticationStateChanged(Task.FromResult(authState));
		}
	}
}
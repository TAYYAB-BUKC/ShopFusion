using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using ShopFusion.Client.Services.Interfaces;
using ShopFusion.Common;
using ShopFusion.Models.DTOs;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace ShopFusion.Client.Services
{
	public class AuthenticationService : IAuthenticationService
	{
		public HttpClient _httpClient { get; set; }
		public ILocalStorageService _localStorageService { get; set; }
		public AuthenticationStateProvider _authenticationStateProvider { get; set; }
		private readonly string _apiBaseURL;

		public AuthenticationService(HttpClient httpClient, ILocalStorageService localStorageService, AuthenticationStateProvider authenticationStateProvider, IConfiguration configuration)
		{
			_httpClient = httpClient;
			_localStorageService = localStorageService;
			_authenticationStateProvider = authenticationStateProvider;
			_apiBaseURL = configuration.GetSection("API_BASEURL").Value;
		}

		public async Task<SignInResponseDTO> Login(SignInRequestDTO signInRequestDTO)
		{
			var content = JsonConvert.SerializeObject(signInRequestDTO);
			var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
			var response = await _httpClient.PostAsync($"{_apiBaseURL}/account/signin", bodyContent);
			var responseContent = await response.Content.ReadAsStringAsync();
			var result = JsonConvert.DeserializeObject<SignInResponseDTO>(responseContent);

			if (result.IsSuccessful)
			{
				await _localStorageService.SetItemAsync(CommonConfiguration.JWTToken_Key, result.Token);
				await _localStorageService.SetItemAsync(CommonConfiguration.UserDetails_Key, result.User);
				((AuthStateProvider)_authenticationStateProvider).NotifyUserLoggedIn(result.Token);
				_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", result.Token);
			}

			return result;
		}

		public async Task Logout()
		{
			await _localStorageService.RemoveItemsAsync(new[] { CommonConfiguration.JWTToken_Key, CommonConfiguration.UserDetails_Key });
			_httpClient.DefaultRequestHeaders.Authorization = null;
			((AuthStateProvider)_authenticationStateProvider).NotifyUserLogout();
		}

		public async Task<SignUpResponseDTO> Register(SignUpRequestDTO signUpRequestDTO)
		{
			var content = JsonConvert.SerializeObject(signUpRequestDTO);
			var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
			var response = await _httpClient.PostAsync($"{_apiBaseURL}/account/signup", bodyContent);
			var responseContent = await response.Content.ReadAsStringAsync();
			var result = JsonConvert.DeserializeObject<SignUpResponseDTO>(responseContent);

			if (response.StatusCode == HttpStatusCode.Created)
			{
				return new SignUpResponseDTO { IsSuccessful = true };
			}

			if (result.IsSuccessful)
			{
				return new SignUpResponseDTO { IsSuccessful = true };
			}

			return result;
		}
	}
}
using Blazored.LocalStorage;
using Newtonsoft.Json;
using ShopFusion.Client.Services.Interfaces;
using ShopFusion.Models.DTOs;
using System.Text;

namespace ShopFusion.Client.Services
{
	public class AuthenticationService : IAuthenticationService
	{
		public HttpClient _httpClient { get; set; }
		public ILocalStorageService _localStorageService { get; set; }
		public AuthStateProvider _authStateProvider { get; set; }

		public AuthenticationService(HttpClient httpClient, ILocalStorageService localStorageService, AuthStateProvider authStateProvider)
		{
			_httpClient = httpClient;
			_localStorageService = localStorageService;
			_authStateProvider = authStateProvider;
		}

		public async Task<SignInResponseDTO> Login(SignInRequestDTO signInRequestDTO)
		{
			var content = JsonConvert.SerializeObject(signInRequestDTO);
			var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
			var response = await _httpClient.PostAsync("/api/account/signin", bodyContent);
			var responseContent = await response.Content.ReadAsStringAsync();
			var result = JsonConvert.DeserializeObject<SignInResponseDTO>(responseContent);

			return result;
		}

		public Task Logout()
		{
			throw new NotImplementedException();
		}

		public Task<SignUpResponseDTO> Register(SignUpRequestDTO signUpRequestDTO)
		{
			throw new NotImplementedException();
		}
	}
}
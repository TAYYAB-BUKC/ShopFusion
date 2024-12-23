using ShopFusion.Models.DTOs;

namespace ShopFusion.Client.Services.Interfaces
{
	public interface IAuthenticationService
	{
		Task<SignUpResponseDTO> Register(SignUpRequestDTO signUpRequestDTO);
		Task<SignInResponseDTO> Login(SignInRequestDTO signInRequestDTO);
		Task Logout();
	}
}
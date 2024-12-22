namespace ShopFusion.Models.DTOs
{
	public class SignInResponseDTO
	{
		public bool IsSuccessful { get; set; }
		public string ErrorMessage { get; set; }
		public string Token { get; set; }
		public UserDTO User { get; set; }
	}
}
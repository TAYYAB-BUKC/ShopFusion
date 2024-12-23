namespace ShopFusion.Models.DTOs
{
	public class SignUpResponseDTO
	{
		public bool IsSuccessful { get; set; }
		public IEnumerable<string> Errors { get; set; }
	}
}
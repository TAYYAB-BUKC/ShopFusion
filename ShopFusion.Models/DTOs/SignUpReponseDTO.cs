namespace ShopFusion.Models.DTOs
{
	public class SignUpReponseDTO
	{
		public bool IsSuccessful { get; set; }
		public IEnumerable<string> Errors { get; set; }
	}
}
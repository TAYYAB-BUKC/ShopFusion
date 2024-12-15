namespace ShopFusion.Models.DTOs
{
	public class SuccessModelDTO
	{
		public string Title { get; set; }
		public int StatusCode { get; set; }
		public string Message { get; set; }
		public object Data { get; set; }
	}
}
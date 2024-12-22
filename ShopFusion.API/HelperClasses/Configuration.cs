namespace ShopFusion.API.HelperClasses
{
	public class Configuration
	{
		public string SecretKey { get; set; }
		public string ValidAudience { get; set; }
		public string ValidIssuer { get; set; }
	}
}
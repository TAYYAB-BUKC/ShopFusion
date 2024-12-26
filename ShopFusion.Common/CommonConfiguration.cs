namespace ShopFusion.Common
{
	public static class CommonConfiguration
	{
		public const string CartKey = "SF_Cart";

		public const string Status_Pending = "Pending";
		public const string Status_Confirmed = "Confirmed";
		public const string Status_Shipped = "Shipped";
		public const string Status_Refunded = "Refunded";
		public const string Status_Cancelled = "Cancelled";

		public const string Role_Admin = "Admin";
		public const string Role_Customer = "Customer";

		public const string JWTToken_Key = "SF_Token";
		public const string UserDetails_Key = "SF_UserDetails";
		public const string OrderDetails_Key = "SF_OrderDetails";
	}
}
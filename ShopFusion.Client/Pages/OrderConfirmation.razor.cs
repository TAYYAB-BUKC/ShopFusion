using ShopFusion.Models.DTOs;

namespace ShopFusion.Client.Pages
{
	public partial class OrderConfirmation
	{
		private bool IsProcessing { get; set; } = false;
		private CustomOrderDTO Order { get; set; }
	}
}
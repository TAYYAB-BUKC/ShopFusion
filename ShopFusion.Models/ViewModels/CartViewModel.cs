using ShopFusion.Models.DTOs;

namespace ShopFusion.Models.ViewModels
{
	public class CartViewModel
	{
		public int ProductId { get; set; }
		public ProductDTO Product { get; set; }
		public int SelectedProductPriceId { get; set; }
		public ProductPricesDTO ProductPrice { get; set; }
		public int Count { get; set; }
	}
}
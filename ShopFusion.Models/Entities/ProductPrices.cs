using System.ComponentModel.DataAnnotations.Schema;

namespace ShopFusion.Models.Entities
{
	public class ProductPrices
	{
		public int Id { get; set; }
		public int ProductId { get; set; }
		[ForeignKey(nameof(ProductId))]
		public Product Product { get; set; }
		public string Size { get; set; }
		public double Price { get; set; }
	}
}

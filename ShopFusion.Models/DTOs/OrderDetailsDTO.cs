using ShopFusion.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace ShopFusion.Models.DTOs
{
	public class OrderDetailsDTO
	{
		public int Id { get; set; }
		[Required]
		public int OrderId { get; set; }
		[Required] 
		public int ProductId { get; set; }
		public ProductDTO Product { get; set; }

		[Required]
		public int Count { get; set; }
		[Required]
		public double Price { get; set; }
		[Required]
		public string Size { get; set; }
		[Required]
		public string ProductName { get; set; }
	}
}

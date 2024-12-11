using ShopFusion.Models.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopFusion.Models.DTOs
{
	public class ProductPricesDTO
	{
		public int Id { get; set; }
		[Required(ErrorMessage = "Please select the product")]
		public int ProductId { get; set; }
		[Required(ErrorMessage = "Please enter the size")]
		public string Size { get; set; }
		[Required]
		[Range(1, Int64.MaxValue, ErrorMessage = "Please enter the correct price")]
		public double Price { get; set; }
	}
}

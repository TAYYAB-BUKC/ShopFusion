using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopFusion.Models.Entities
{
	public class OrderDetails
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public int OrderId { get; set; }
		public int ProductId { get; set; }
		[ForeignKey("ProductId")]
		[NotMapped]
		public virtual Product Product { get; set; }
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

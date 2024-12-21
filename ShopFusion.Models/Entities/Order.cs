using System.ComponentModel.DataAnnotations;

namespace ShopFusion.Models.Entities
{
	public class Order
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public int UserId { get; set; }
		[Required]
		public double GrandTotal { get; set; }
		[Required]
		public DateTime OrderDate { get; set; }
		[Required]
		public DateTime ShippingDate { get; set; }
		[Required] 
		public string Status { get; set; }
		
		public string? SessionId { get; set; }
		public string? PaymentIntentId { get; set; }

		[Required]
		public string Name { get; set; }		
		[Required]
		public string? PhoneNumber { get; set; }
		[Required]
		public string? StreetAddress { get; set; }
		[Required]
		public string State { get; set; }
		[Required]
		public string City { get; set; }
		[Required]
		public string PostalCode { get; set; }
		[Required]
		public string Email { get; set; }
	}

	public class CustomOrder
	{
		public Order Order { get; set; }
		public List<OrderDetails> OrderDetails { get; set; }
	}
}
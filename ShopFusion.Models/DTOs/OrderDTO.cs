using System.ComponentModel.DataAnnotations;

namespace ShopFusion.Models.DTOs
{
	public class OrderDTO
	{
		public int Id { get; set; }
		[Required]
		public string UserId { get; set; }
		[Required]
		[Display(Name = "Grand Total")]
		public double GrandTotal { get; set; }
		[Required]
		[Display(Name = "Order Date")]
		public DateTime OrderDate { get; set; }
		[Required]
		[Display(Name = "Shipping Date")]
		public DateTime ShippingDate { get; set; }
		[Required]
		public string Status { get; set; }

		public string? SessionId { get; set; }
		public string? PaymentIntentId { get; set; }

		[Required]
		public string Name { get; set; }
		[Required]
		[Display(Name = "Phone Number")]
		public string? PhoneNumber { get; set; }
		[Required]
		[Display(Name = "Street Address")]
		public string? StreetAddress { get; set; }
		[Required]
		public string State { get; set; }
		[Required]
		public string City { get; set; }
		[Required]
		[Display(Name = "Postal Code")]
		public string PostalCode { get; set; }
		[Required]
		public string Email { get; set; }
		public string? Tracking { get; set; }
		public string? Carrier { get; set; }
	}

	public class CustomOrderDTO
	{
		public OrderDTO Order { get; set; }
		public List<OrderDetailsDTO> OrderDetails { get; set; }
	}
}

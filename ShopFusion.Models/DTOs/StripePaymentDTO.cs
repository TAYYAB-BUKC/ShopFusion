using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopFusion.Models.DTOs
{
	public class StripePaymentDTO
	{
		public StripePaymentDTO()
		{
			SuccessURL = "OrderConfirmation";
			CancelURL = "Checkout";
		}

		public CustomOrderDTO Order { get; set; }
		public string SuccessURL { get; set; }
		public string CancelURL { get; set; }
	}
}
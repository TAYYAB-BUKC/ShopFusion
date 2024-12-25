using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopFusion.Models.DTOs
{
	public class StripePaymentDTO
	{
		public CustomOrderDTO Order { get; set; }
	}
}
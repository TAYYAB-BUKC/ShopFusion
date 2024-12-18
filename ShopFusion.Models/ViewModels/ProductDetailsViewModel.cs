using ShopFusion.Models.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopFusion.Models.ViewModels
{
	public class ProductDetailsViewModel
	{
		[Range(1, Int64.MaxValue, ErrorMessage = "Please enter a value greater than 0.")]
		public int Count { get; set; }
		[Required]
		public int SelectedProductPriceID { get; set; }
		public ProductPricesDTO	ProductPrice { get; set; }

		public ProductDetailsViewModel()
		{
			Count = 1;
			SelectedProductPriceID = 0;
			ProductPrice = new ProductPricesDTO();
		}
	}
}

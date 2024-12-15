using ShopFusion.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopFusion.Models.DTOs
{
	public class ProductDTO
	{
		public int Id { get; set; }
		[Required(ErrorMessage = "Please enter the name")]
		public string Name { get; set; }
		[Required(ErrorMessage = "Please enter the description")]
		public string Description { get; set; }
		public bool IsShopFavorite { get; set; }
		public bool IsCustomerFavorite { get; set; }
		public string Color { get; set; }
		public string ImageURL { get; set; }
		[Range(1, Int64.MaxValue, ErrorMessage = "Please select the category")]
		public int CategoryId { get; set; }
		public CategoryDTO Category { get; set; }
		public ICollection<ProductPrices> ProductPrices { get; set; }
	}
}

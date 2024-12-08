using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopFusion.Models.Entities
{
	//public class Product
	//{
	//	public int Id { get; set; }
	//	public string Name { get; set; }
	//	public bool IsActive { get; set; }
	//	public double Price { get; set; }
	//	public IEnumerable<ProductProperties> Attributes { get; set; }
	//}

	public class Product
    {
		[Key]
        public int Id { get; set; }
        public string Name { get; set; }
		public string Description { get; set; }
		public bool IsShopFavorite{ get; set; }
		public bool IsCustomerFavorite { get; set; }
        public string Color { get; set; }
		public string ImageURL { get; set; }
		public int CategoryId { get; set; }
		[ForeignKey("CategoryId")]
		public Category Category { get; set; }
	}

    public class ProductProperties
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
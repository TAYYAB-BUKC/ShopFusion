using System.ComponentModel.DataAnnotations;

namespace ShopFusion.Models.Entities
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}

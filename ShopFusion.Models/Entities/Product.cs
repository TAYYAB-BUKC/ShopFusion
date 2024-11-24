namespace ShopFusion.Models.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public double Price { get; set; }
        public IEnumerable<ProductProperties> Attributes { get; set; }
    }

    public class ProductProperties
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
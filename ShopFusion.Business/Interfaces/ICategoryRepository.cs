using ShopFusion.Models.DTOs;

namespace ShopFusion.Business.Interfaces
{
    public interface ICategoryRepository
    {
        public List<CategoryDTO> GetAll();
        public CategoryDTO GetById(int id);
        public CategoryDTO Create(CategoryDTO category);
        public CategoryDTO Update(CategoryDTO category);
        public CategoryDTO Delete(int id);
    }
}
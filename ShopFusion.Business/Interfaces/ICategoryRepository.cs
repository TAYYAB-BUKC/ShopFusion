using ShopFusion.Models.DTOs;

namespace ShopFusion.Business.Interfaces
{
    public interface ICategoryRepository
    {
        public Task<List<CategoryDTO>> GetAll();
        public Task<CategoryDTO> GetById(int id);
        public Task<CategoryDTO> Create(CategoryDTO category);
        public Task<CategoryDTO> Update(CategoryDTO category);
        public Task<int> Delete(int id);
    }
}
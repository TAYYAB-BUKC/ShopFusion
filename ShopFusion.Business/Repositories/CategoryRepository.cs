using ShopFusion.Business.Interfaces;
using ShopFusion.DataAccess.Data;
using ShopFusion.Models.DTOs;
using ShopFusion.Models.Entities;

namespace ShopFusion.Business.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private ApplicationDbContext _dbContext;
        public CategoryRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public CategoryDTO Create(CategoryDTO categoryDTO)
        {
            Category category = new Category() 
            { 
                Id = categoryDTO.Id,
                Name = categoryDTO.Name,
                CreatedDate = DateTime.Now
            };

            _dbContext.Categories.Add(category);
            _dbContext.SaveChanges();

            return new CategoryDTO
            {
                Id = category.Id,
                Name = category.Name
            };
        }

        public CategoryDTO Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<CategoryDTO> GetAll()
        {
            throw new NotImplementedException();
        }

        public CategoryDTO GetById(int id)
        {
            throw new NotImplementedException();
        }

        public CategoryDTO Update(CategoryDTO category)
        {
            throw new NotImplementedException();
        }
    }
}

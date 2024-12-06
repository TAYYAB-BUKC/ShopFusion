using AutoMapper;
using ShopFusion.Business.Interfaces;
using ShopFusion.DataAccess.Data;
using ShopFusion.Models.DTOs;
using ShopFusion.Models.Entities;
using ShopFusion.Models.Mappers;

namespace ShopFusion.Business.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _dbContext;
		private readonly IMapper _mapper;

		public CategoryRepository(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public CategoryDTO Create(CategoryDTO categoryDTO)
        {
            Category category = _mapper.Map<CategoryDTO, Category>(categoryDTO);
            
            _dbContext.Categories.Add(category);
            _dbContext.SaveChanges();

            return _mapper.Map<Category, CategoryDTO>(category);
		}

        public int Delete(int id)
        {
            var obj = _dbContext.Categories.FirstOrDefault(c => c.Id == id);
            if(obj != null)
            {
                _dbContext.Categories.Remove(obj);
                return _dbContext.SaveChanges();
            }
            return 0;
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

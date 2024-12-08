using AutoMapper;
using Microsoft.EntityFrameworkCore;
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

        public async Task<CategoryDTO> Create(CategoryDTO categoryDTO)
        {
            Category category = _mapper.Map<CategoryDTO, Category>(categoryDTO);
            
            _dbContext.Categories.Add(category);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<Category, CategoryDTO>(category);
		}

        public async Task<int> Delete(int id)
        {
            var obj = await _dbContext.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if(obj != null)
            {
                _dbContext.Categories.Remove(obj);
                return await _dbContext.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<List<CategoryDTO>> GetAll()
        {
            return _mapper.Map<IEnumerable<Category>, List<CategoryDTO>>(await _dbContext.Categories.ToListAsync());
        }

        public async Task<CategoryDTO> GetById(int id)
        {
			var obj = await _dbContext.Categories.FirstOrDefaultAsync(c => c.Id == id);
			if (obj != null)
			{
                return _mapper.Map<Category, CategoryDTO>(obj);
			}
			return new CategoryDTO();
		}

        public async Task<CategoryDTO> Update(CategoryDTO category)
        {
			var objFromDB = await _dbContext.Categories.FirstOrDefaultAsync(c => c.Id == category.Id);
			if (objFromDB != null)
			{
                objFromDB.Name = category.Name;
                _dbContext.Categories.Update(objFromDB);
                await _dbContext.SaveChangesAsync();
				return _mapper.Map<Category, CategoryDTO>(objFromDB);
			}
			return category;
		}
    }
}

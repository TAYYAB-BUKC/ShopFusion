using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShopFusion.Business.Interfaces;
using ShopFusion.DataAccess.Data;
using ShopFusion.Models.DTOs;
using ShopFusion.Models.Entities;
using System.Collections.Generic;

namespace ShopFusion.Business.Repositories
{
	public class ProductRepository : IProductRepository
	{
		private readonly ApplicationDbContext _dbContext;
		private readonly IMapper _mapper;

		public ProductRepository(ApplicationDbContext dbContext, IMapper mapper)
		{
			_dbContext = dbContext;
			_mapper = mapper;
		}

		public async Task<ProductDTO> Create(ProductDTO productDTO)
		{
			Product product = _mapper.Map<ProductDTO, Product>(productDTO);

			_dbContext.Products.Add(product);
			await _dbContext.SaveChangesAsync();

			return _mapper.Map<Product, ProductDTO>(product);
		}

		public async Task<int> Delete(int id)
		{
			Product product = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == id);
			if (product != null)
			{
				_dbContext.Products.Remove(product);
				await _dbContext.SaveChangesAsync();
			}
			return 0;
		}

		public async Task<List<ProductDTO>> GetAll()
		{
			return _mapper.Map<IEnumerable<Product>, List<ProductDTO>>(await _dbContext.Products.Include(p => p.Category).ToListAsync());
		}

		public async Task<ProductDTO> GetById(int id)
		{
			Product product = await _dbContext.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);
			if (product != null)
			{
				return _mapper.Map<Product, ProductDTO>(product);
			}
			return new ProductDTO();
		}

		public async Task<ProductDTO> Update(ProductDTO productDTO)
		{
			Product productFromDB = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == productDTO.Id);
			if (productFromDB != null)
			{
				//productFromDB = _mapper.Map<ProductDTO, Product>(productDTO);
				//_dbContext.Products.Update(_mapper.Map<ProductDTO, Product>(productDTO));
				//_dbContext.Entry(productFromDB).State = EntityState.Modified;
				productFromDB.Name = productDTO.Name;
				productFromDB.Description = productDTO.Description;
				productFromDB.IsShopFavorite = productDTO.IsShopFavorite;
				productFromDB.IsCustomerFavorite = productDTO.IsCustomerFavorite;
				productFromDB.Color = productDTO.Color;
				productFromDB.ImageURL = productDTO.ImageURL;
				productFromDB.CategoryId = productDTO.CategoryId;
				_dbContext.Products.Update(productFromDB);
				await _dbContext.SaveChangesAsync();
			}
			return _mapper.Map<Product, ProductDTO>(productFromDB);
		}
	}
}

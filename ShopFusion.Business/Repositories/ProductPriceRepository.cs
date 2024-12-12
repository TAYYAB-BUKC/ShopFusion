using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShopFusion.Business.Interfaces;
using ShopFusion.DataAccess.Data;
using ShopFusion.Models.DTOs;
using ShopFusion.Models.Entities;

namespace ShopFusion.Business.Repositories
{
	public class ProductPriceRepository : IProductPriceRepository
	{
		private readonly ApplicationDbContext _dbContext;
		private readonly IMapper _mapper;

		public ProductPriceRepository(ApplicationDbContext dbContext, IMapper mapper)
		{
			_dbContext = dbContext;
			_mapper = mapper;
		}

		public async Task<ProductPricesDTO> Create(ProductPricesDTO productPricesDTO)
		{
			var productPrices = _mapper.Map<ProductPricesDTO, ProductPrices>(productPricesDTO);

			await _dbContext.ProductPrices.AddAsync(productPrices);
			await _dbContext.SaveChangesAsync();

			return _mapper.Map<ProductPrices, ProductPricesDTO>(productPrices);
		}

		public async Task<int> Delete(int id)
		{
			var productPrice = await _dbContext.ProductPrices.FirstOrDefaultAsync(pp => pp.Id == id);
			if(productPrice != null)
			{
				_dbContext.ProductPrices.Remove(productPrice);
				return await _dbContext.SaveChangesAsync();
			}
			return 0;
		}

		public async Task<List<ProductPricesDTO>> GetAll(int? productID = null)
		{
			if (productID is null)
			{
				return _mapper.Map<List<ProductPrices>, List<ProductPricesDTO>>(await _dbContext.ProductPrices.ToListAsync());
			}
			else
			{
				return _mapper.Map<List<ProductPrices>, List<ProductPricesDTO>>(await _dbContext.ProductPrices.Where(pp => pp.ProductId == productID).ToListAsync());
			}
		}

		public async Task<ProductPricesDTO> GetById(int id)
		{
			var productPrice = await _dbContext.ProductPrices.FirstOrDefaultAsync(pp => pp.Id == id);
			if(productPrice != null)
			{
				return _mapper.Map<ProductPrices, ProductPricesDTO>(productPrice);
			}

			return new ProductPricesDTO();
		}

		public async Task<ProductPricesDTO> Update(ProductPricesDTO productPricesDTO)
		{
			var productPricesFromDB = await _dbContext.ProductPrices.FirstOrDefaultAsync(pp => pp.Id == productPricesDTO.Id);
			if(productPricesFromDB != null)
			{
				productPricesFromDB.ProductId = productPricesDTO.ProductId;
				productPricesFromDB.Size = productPricesDTO.Size;
				productPricesFromDB.Price = productPricesDTO.Price;

				_dbContext.ProductPrices.Update(productPricesFromDB);
				await _dbContext.SaveChangesAsync();
			}

			return productPricesDTO;
		}
	}
}
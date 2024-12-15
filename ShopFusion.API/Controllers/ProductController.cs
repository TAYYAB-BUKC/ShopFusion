using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopFusion.Business.Interfaces;
using ShopFusion.Models.DTOs;

namespace ShopFusion.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductController : ControllerBase
	{
		public IProductRepository _productRepository { get; set; }
		public ProductController(IProductRepository productRepository)
		{
			_productRepository = productRepository;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll() 
		{
			return Ok(await _productRepository.GetAll());
		}

		[HttpGet("{productId}")]
		public async Task<IActionResult> GetById(int? productId)
		{
			if(productId == null || productId == 0)
			{
				return BadRequest(new ErrorModelDTO()
				{
					Message = "Please specify product id",
					StatusCode = StatusCodes.Status400BadRequest
				});
			}

			var product = await _productRepository.GetById(productId.Value);
			if(product.Id <= 0)
			{
				return BadRequest(new ErrorModelDTO()
				{
					Message = "Product not found",
					StatusCode = StatusCodes.Status404NotFound
				});
			}

			return Ok(product);
		}
	}
}

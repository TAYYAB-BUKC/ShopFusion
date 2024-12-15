using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopFusion.Business.Interfaces;

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
	}
}

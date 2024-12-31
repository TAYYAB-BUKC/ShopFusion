using Microsoft.AspNetCore.Components;
using ShopFusion.Client.Services;
using ShopFusion.Client.Services.Interfaces;
using ShopFusion.Models.DTOs;

namespace ShopFusion.Client.Pages
{
	public partial class Index
	{
		[Inject]
		public IProductService ProductService { get; set; }

		public bool IsProcessing { get; set; }
		public IEnumerable<ProductDTO> products = new List<ProductDTO>();

		protected override async Task OnInitializedAsync()
		{
			IsProcessing = true;
			products = await ProductService.GetAllProducts();
			IsProcessing = false;
		}
	}
}

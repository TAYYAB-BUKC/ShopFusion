using AutoMapper;
using ShopFusion.Models.DTOs;
using ShopFusion.Models.Entities;

namespace ShopFusion.Models.Mappers
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<Category, CategoryDTO>().ReverseMap();
			CreateMap<Product, ProductDTO>().ReverseMap();
			CreateMap<ProductPrices, ProductPricesDTO>().ReverseMap();
			CreateMap<Order, OrderDTO>().ReverseMap();
			CreateMap<OrderDetails, OrderDetailsDTO>().ReverseMap();
			CreateMap<CustomOrder, CustomOrderDTO>().ReverseMap();
		}
	}
}
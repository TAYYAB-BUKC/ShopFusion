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
		}
	}
}

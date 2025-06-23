using AutoMapper;
using ECommerce.Application.DTOs.ProductDTOs;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.Mappings;

public class ProductProfile : Profile
{
	public ProductProfile()
	{
		CreateMap<Product, ProductDto>()
			.ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category!.Name));

		CreateMap<CreateProductDto, Product>();

		CreateMap<UpdateProductDto, Product>();
	}
}

using AutoMapper;
using DomainLayer.Models;
using Shared.ProductsDtos;

namespace Service.MappingProfiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<CreatedProductsDto, Products>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.description))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.price))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.category))
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.imageUrl));

            CreateMap<Products, ProductsDto>()
               .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.name, opt => opt.MapFrom(src => src.ProductName))
               .ForMember(dest => dest.description, opt => opt.MapFrom(src => src.Description))
               .ForMember(dest => dest.price, opt => opt.MapFrom(src => src.Price))
               .ForMember(dest => dest.category, opt => opt.MapFrom(src => src.Category))
               .ForMember(dest => dest.imageUrl, opt => opt.MapFrom(src => src.ImageUrl));
            CreateMap<UpdatedProductDto, Products>();
        }
    }
}

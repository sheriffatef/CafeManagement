using AutoMapper;
using DomainLayer.Models;
using Shared.OrdersDtos;
using Shared.ProductsDtos;

namespace Service.MappingProfiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<CreatedOrderDto, Orders>()
           .ForMember(dest => dest.TableId, opt => opt.MapFrom(src => src.TableId))
           .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.Items))
           .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(_ => DateTime.UtcNow))
           .ForMember(dest => dest.Status, opt => opt.MapFrom(_ => "preparing"))
           .ForMember(dest => dest.GuestName, opt => opt.MapFrom(src => src.GuestName))
           .ForMember(dest => dest.TotalAmount, opt => opt.Ignore())
           .ForMember(dest => dest.UserId, opt => opt.Ignore())
           .ForMember(dest => dest.User, opt => opt.Ignore())
           .ForMember(dest => dest.Table, opt => opt.Ignore());

            CreateMap<CreatedOrderItemDto, OrderItems>()
           .ForMember(dest => dest.Price, opt => opt.Ignore())
           .ForMember(dest => dest.OrderId, opt => opt.Ignore())
           .ForMember(dest => dest.Order, opt => opt.Ignore())

           .ForMember(dest => dest.Product, opt => opt.Ignore());

            CreateMap<Orders, OrderDto>()
           .ForMember(dest => dest.total, opt => opt.MapFrom(src => src.TotalAmount))
           .ForMember(dest => dest.items, opt => opt.MapFrom(src => src.OrderItems ?? new List<OrderItems>()))
           .ForMember(dest => dest.GuestName, opt => opt.MapFrom(src => src.GuestName))
           .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(src => src.OrderDate));



            CreateMap<OrderItems, OrderItemsDto>()
           .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.ProductName));


        }
    }
}

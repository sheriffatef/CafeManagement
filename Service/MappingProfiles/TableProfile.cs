using AutoMapper;
using DomainLayer.Models;
using Shared.TablesDtos;

namespace Service.MappingProfiles
{
    public class TableProfile : Profile
    {
        public TableProfile()
        {
            CreateMap<CreatedTableDto, Tables>()
            .ForMember(dest => dest.TableName, opt => opt.MapFrom(src => src.name))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.status));
            CreateMap<UpdatedTableDto, Tables>();

            CreateMap<Tables, TableDto>()
          .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.Id))
          .ForMember(dest => dest.name, opt => opt.MapFrom(src => src.TableName))
          .ForMember(dest => dest.capacity, opt => opt.MapFrom(src => src.Capacity))
          .ForMember(dest => dest.status, opt => opt.MapFrom(src => src.Status));
        }
    }
}

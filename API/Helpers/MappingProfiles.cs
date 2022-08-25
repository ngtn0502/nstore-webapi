using API.Dtos;
using AutoMapper;
using Core.Entities;

namespace API.Helpers
{
   public class MappingProfiles : Profile
   {
      public MappingProfiles()
      {
         CreateMap<Product, ProductToReturnDto>()
            .ForMember(p => p.ProductBrand, o => o.MapFrom(p => p.ProductBrand.Name))
            .ForMember(p => p.ProductType, o => o.MapFrom(p => p.ProductType.Name))
            .ForMember(p => p.ProductSize, o => o.MapFrom(p => p.ProductSize.Size))
            .ForMember(p => p.PictureUrl, o => o.MapFrom<ProductUrlResolver>());
      }
   }
}
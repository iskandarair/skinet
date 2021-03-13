using AutoMapper;
using Core.Entities;
using Core.Entities.Identity;
using Skinet.Dtos;

namespace Skinet.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(x => x.ProductBrand, o => o.MapFrom(s => s.ProductBrand.Name)).
                ForMember(x => x.ProductType, o => o.MapFrom(s => s.ProductType.Name)).
                ForMember(d => d.PictureUrl, o => o.MapFrom<ProductUrlRessolver>());
            CreateMap<Address, AddresDto>().ReverseMap();
            CreateMap<BasketItemDto, BasketItem>().ReverseMap();
            CreateMap<CustomerBasketDto, CustomerBasket>().ReverseMap();
        }
    }
}

using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Entities.Identity;
using Core.Entities.OrderAggregate;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturnDto>()
            //eksplicitno kažemo automapperu, productbrand je string u dtou, moramo ga uputiti
                .ForMember(d => d.ProductBrand, o => o.MapFrom(s => s.ProductBrand.Name))
                .ForMember(d => d.ProductType, o => o.MapFrom(s => s.ProductType.Name))
                .ForMember(d => d.PictureUrl, o => o.MapFrom<ProductUrlResolver>());    
                CreateMap<Core.Entities.Identity.Address, AddressDto>().ReverseMap();               
                CreateMap<CustomerBasketDto, CustomerBasket>();
                CreateMap<BasketItemDto, BasketItem>();
                //mapiraš u drugu adresu, ono gore je adresa iz identity
                CreateMap<AddressDto, Core.Entities.OrderAggregate.Address>();
                CreateMap<Order, OrderToReturnDto>()
                .ForMember(d => d.DeliveryMethod, o => o.MapFrom(s => s.DeliveryMethod.ShortName))
                .ForMember(d => d.ShippingPrice, o => o.MapFrom(s => s.DeliveryMethod.Price));
                 CreateMap<OrderItem, OrderItemDto>()
                .ForMember(d => d.ProductId, o => o.MapFrom(s =>  s.ItemOrdered.ProductItemId))
                .ForMember(d => d.ProductName, o => o.MapFrom(s =>  s.ItemOrdered.ProductName))
                .ForMember(d => d.PictureUrl, o => o.MapFrom(s =>  s.ItemOrdered.PictureUrl))
                //we are puting this because we need api url of our server in front of picture
                .ForMember(d => d.PictureUrl, o => o.MapFrom<OrderItemUrlResolver>());
               }
    }
}










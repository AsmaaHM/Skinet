using AutoMapper;
using Skinet.API.Dtos;
using Skinet.Core;
using Skinet.Core.Entities;
using Skinet.Core.Entities.Identity;
using Skinet.Core.Entities.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skinet.API.Helpers
{
	public class MappingProfiles : Profile
	{
		public MappingProfiles()
		{
			CreateMap<Product, ProductToReturnDto>()
				.ForMember(product=> product.Brand, dest=> dest.MapFrom(source=> source.Brand.Name))
				.ForMember(product => product.Type, dest => dest.MapFrom(source => source.Type.Name))
				.ForMember(product => product.PictureUrl, dest => dest.MapFrom<ProductUrlResolver>());

			CreateMap<Core.Entities.Identity.Address, AddressDto>().ReverseMap();
			CreateMap<CustomerBasketDto, CustomerBasket>().ReverseMap();
			CreateMap<BasketItemDto, BasketItem>().ReverseMap();
			CreateMap<AddressDto, Core.Entities.OrderAggregate.Address>();
			
			CreateMap<Order, OrderToReturnDto>()
				.ForMember(order=> order.DeliveryMethod, o=> o.MapFrom(s=> s.DeliveryMethod.ShortName))
				.ForMember(order=> order.ShippingPrice, o=> o.MapFrom(s=> s.DeliveryMethod.Price));

			CreateMap<OrderItem, OrderItemDto>()
				.ForMember(item => item.ProductId, o => o.MapFrom(s => s.ItemOrdered.ProductItemId))
				.ForMember(item => item.ProductName, o => o.MapFrom(s => s.ItemOrdered.ProductName))
				.ForMember(item => item.PictureUrl, o => o.MapFrom(s => s.ItemOrdered.PictureUrl))
				.ForMember(item => item.PictureUrl, o => o.MapFrom<OrderItemUrlResolver>());


		}
	}
}

using AutoMapper;
using Skinet.API.Dtos;
using Skinet.Core;
using Skinet.Core.Entities.Identity;
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

			CreateMap<Address, AddressDto>().ReverseMap(); 
		}
	}
}

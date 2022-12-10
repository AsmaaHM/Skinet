using AutoMapper;
using AutoMapper.Execution;
using AutoMapper.Internal;
using Microsoft.Extensions.Configuration;
using Skinet.API.Dtos;
using Skinet.Core;
using Skinet.Core.Entities.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace Skinet.API.Helpers
{
	public class OrderItemUrlResolver : IValueResolver<OrderItem, OrderItemDto, string>
	{
		private readonly IConfiguration _config;

		public OrderItemUrlResolver(IConfiguration config)
		{
			_config = config; 
		}
		public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
		{
			if (!string.IsNullOrEmpty(source.ItemOrdered.PictureUrl))
				return _config["ApiUrl"] + source.ItemOrdered.PictureUrl;

			return null; 
		}
	}
}

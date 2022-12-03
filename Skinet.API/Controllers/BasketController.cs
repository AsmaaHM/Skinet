﻿using AutoMapper;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Skinet.API.Dtos;
using Skinet.API.Errors;
using Skinet.API.Helpers;
using Skinet.Core;
using Skinet.Core.Entities;
using Skinet.Core.Interfaces;
using Skinet.Core.Specifications;
using Skinet.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skinet.API.Controllers
{
	public class BasketController : BaseApiController
	{

		private readonly IBasketRepository _basketRepository;

		public BasketController(IBasketRepository basketRepository)
		{
			_basketRepository = basketRepository;
		}

		[HttpGet]
		public async Task<ActionResult<CustomerBasket>> GetBasketById(string id) 
		{
			var basket = await _basketRepository.GetBasketAsync(id);
			return Ok(basket ?? new CustomerBasket(id));
		}

		[HttpPost]
		public async Task<ActionResult<CustomerBasket>>UpdateBasket(CustomerBasket basket)
		{
			var updatedBasket = await _basketRepository.UpdateBasketAsync(basket);
			return Ok(updatedBasket);
		}

		[HttpDelete]
		public async Task DeleteBasket(string id)
		{
			await _basketRepository.DeleteBAsketAsync(id);
		}

	} 
}
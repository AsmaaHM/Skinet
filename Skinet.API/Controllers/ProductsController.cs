﻿using AutoMapper;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Skinet.API.Dtos;
using Skinet.API.Errors;
using Skinet.Core;
using Skinet.Core.Specifications;
using Skinet.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skinet.API.Controllers
{
	public class ProductsController : BaseApiController
	{
		private readonly IGenericRepository<Product> _productsRepo;
		private readonly IGenericRepository<ProductType> _productTypesRepo;
		private readonly IGenericRepository<ProductBrand> _productBrandsRepo;
		private readonly IMapper _mapper;

		public ProductsController(IGenericRepository<Product> productsRepo, 
			IGenericRepository<ProductBrand> productBrandsRepo, 
			IGenericRepository<ProductType> productTypesRepo, 
			IMapper mapper)
		{
			_productsRepo = productsRepo;
			_productTypesRepo = productTypesRepo;
			_productBrandsRepo = productBrandsRepo;
			_mapper = mapper;
		}

		[HttpGet]
		public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> Get() {

			var specification = new ProductWithBrandAndType();
			var products = await _productsRepo.ListAsync(specification);
			return Ok(_mapper.Map< IReadOnlyList<Product>, 
				IReadOnlyList<ProductToReturnDto>>(products));
		}

		[HttpGet]
		[Route("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
		public async Task<ActionResult<ProductToReturnDto>> Get(int id)
		{
			var specification = new ProductWithBrandAndType(id);
			var product = await _productsRepo.GetEntityWithSpec(specification);

			if (product == null)
				return NotFound(new ApiResponse(404));

			return _mapper.Map<Product, ProductToReturnDto>(product);
		}


	} 
}

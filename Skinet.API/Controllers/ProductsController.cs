using AutoMapper;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Skinet.API.Dtos;
using Skinet.API.Errors;
using Skinet.API.Helpers;
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
		private readonly IGenericRepository<ProductBrand> _brandsRepo;
		private readonly IGenericRepository<ProductType> _typesRepo;
		private readonly IMapper _mapper;

		public ProductsController(IGenericRepository<Product> productsRepo, 
			IGenericRepository<ProductType> typesRepo, 
			IMapper mapper, 
			IGenericRepository<ProductBrand> brandsRepo)
		{
			_productsRepo = productsRepo;
			_mapper = mapper;
			_brandsRepo = brandsRepo;
			_typesRepo = typesRepo;
		}

		[HttpGet]
		public async Task<ActionResult<Pagination<ProductToReturnDto>>> 
			Get([FromQuery]ProductSpecParams productParams) 
		{
			var specification = new ProductWithBrandAndType(productParams);
			var countSpecification = new ProductWithFiltersForCountAndSepcification(productParams);
			var totalItems = await _productsRepo.CountAsync(countSpecification);

			var products = await _productsRepo.ListAsync(specification);
			var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);
			return Ok(new Pagination<ProductToReturnDto>(productParams.PageIndex, productParams.PageSize, totalItems, data));
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

		[HttpGet("brands")]
		public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrands()
		{
			var brands = await _productsRepo.ListAllAsync();
			return Ok(brands);
		}

		[HttpGet("types")]
		public async Task<ActionResult<IReadOnlyList<ProductType>>> GetTypes()
		{
			var types = await _typesRepo.ListAllAsync();
			return Ok(types);
		}


	} 
}

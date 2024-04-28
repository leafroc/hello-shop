﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiTenancySample.FieldIsolationService.Entities;
using MultiTenancySample.FieldIsolationService.EntityFrameworks;

namespace MultiTenancySample.FieldIsolationService.Controllers
{
    [Route("api/{tenant}/[controller]")]
    [ApiController]
    public class ProductsController(IDbContextFactory<FieldIsolationServiceDbContext> dbContextFactory) : ControllerBase
    {
        private readonly FieldIsolationServiceDbContext _dbContext = dbContextFactory.CreateDbContext();

        [HttpGet]
        public IActionResult GetProducts(string? keyword)
        {
            IQueryable<Product> products = _dbContext.Set<Product>();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                products = products.Where(p => p.Name.Contains(keyword));
            }

            return Ok(products);
        }
    }
}

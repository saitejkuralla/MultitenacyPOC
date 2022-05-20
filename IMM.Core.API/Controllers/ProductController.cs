using IMM.Core.API.DTO;
using IMM.Core.API.JWT;
using IMM.Core.API.JWT.Models;
using IMM.Core.API.Repository;
using IMM.EntityFrameworkCore.SQL;
using IMM.EntityFrameworkCore.SQL.Data;
using IMM.MultiTenancy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;

namespace IMM.Core.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ICurrentTenant _currentTenant;
        private readonly ILogger<ProductController> _logger;
        public ProductController(ILogger<ProductController> logger,
          ICurrentTenant currentTenant, ApplicationDbContext context)
        {
            _logger = logger;
            _currentTenant = currentTenant;
            _context = context;
        }

        [HttpGet(Name = "GetProducts")]
        public APITenantInfo Get()
        {
            List<ProductDTO> Products = GetProducts();


            var result = new APITenantInfo()
            {
                Id = _currentTenant.Id.Value,
                Name = _currentTenant.Name,
                Products = Products
            };

            return result;
        }

        #region Getproducts
        private List<ProductDTO> GetProducts()
        {
            var model = _context.Products
                                .Include(b => b.Category)
                                .ToList();

            var Products = new List<ProductDTO>();

            var product = new ProductDTO();
            foreach (var item in model)
            {
                product.ID = item.Id;
                product.Name = item.Name;
                product.Description = item.Description;
                Products.Add(product);

            }

            return Products;
        }
        #endregion

    }
}
﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMM.EntityFrameworkCore.SQL.Data
{
    public class ProductCategory : BaseEntity
    {
        private readonly List<Product> _products = new List<Product>();

        public ProductCategory()
        {

        }
        public ProductCategory(int tenantId, int id, string name)
        {
            TenantId = tenantId;
            Id = id;
            Name = name;
        }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public IEnumerable<Product> Products => _products.AsReadOnly();

        public void AddProduct(Product product)
        {
            product.Category = this;

            _products.Add(product);
        }
    }
}

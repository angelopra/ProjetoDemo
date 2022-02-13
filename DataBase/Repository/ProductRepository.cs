using DataBase.Context;
using DataBase.Repository.Base;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataBase.Repository
{
    public class ProductRepository : BaseRepository<CoreDbContext>, IProductRepository
    {
        public ProductRepository(CoreDbContext context) : base(context)
        {
        }

        public async Task<int> AddProduct(Product request)
        {
            try
            {
                 await _context.Product.AddAsync(request);
                await _context.SaveChangesAsync();
                return request.Id;
            }
            catch
            {
                throw;
            }
        }

        public Product GetProductById(int id)
        {
            try
            {
                var product = _context.Product
                                    .Where(c => c.Id == id)
                                    .Include(p => p.Category)
                                    .FirstOrDefault();
                if (product == null)
                {
                    throw new Exception("Product doesn't exist");
                }
                return product;
            }
            catch
            {
                throw;
            }
        }

        public IQueryable<Product> GetProductsByCategoryId(int categoryId)
        {
            try
            {
                var products = _context.Product.Where(p => p.Category.Id == categoryId);
                var category = _context.Category.Where(c => c.Id == categoryId).FirstOrDefault();
                if (category == null)
                {
                    throw new Exception("Category doesn't exist");
                }
                return products;
            }
            catch
            {
                throw;
            }
        }

        public void Remove(int id)
        {
            try
            {
                var product = _context.Product.Where(c => c.Id == id).FirstOrDefault();
                if (product == null)
                {
                    throw new Exception("Product doesn't exist");
                }
                _context.Product.Remove(product);
                _context.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public Product Update(Product product)
        {
            try
            {
                if (_context.Product.Where(p => p.Id == product.Id).FirstOrDefault() == null)
                {
                    throw new Exception("Product doesn't exist");
                }
                _context.Product.Update(product);
                _context.SaveChanges();
                return product;
            }
            catch
            {
                throw;
            }
        }
    }
}

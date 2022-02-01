using DataBase.Context;
using DataBase.Repository.Base;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataBase.Repository
{
    public class ProductRepository : BaseRepository<CoreDbContext>, IProductRepository
    {
        public ProductRepository(CoreDbContext context) : base(context)
        {
        }

        public int AddProduct(Product request)
        {
            try
            {
                _context.Product.Add(request);
                _context.SaveChanges();
                return request.Id;
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public Product GetProductById(int id)
        {
            try
            {
                Product product = this._context.Product
                                    .Where(c => c.Id == id)
                                    .Include(p => p.Category)
                                    .FirstOrDefault();
                if (product != null)
                {
                    return product;
                }
                else
                {
                    throw new Exception("Product doesn't exist");
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public List<Product> GetProductsByCategoryId(int categoryId)
        {
            try
            {
                var products = _context.Product.Where(p => p.Category.Id == categoryId).ToList();
                var category = _context.Category.Where(c => c.Id == categoryId).FirstOrDefault();
                if (category == null)
                {
                    throw new Exception("Category doesn't exist");
                }
                return products;
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public void Remove(int id)
        {
            try
            {
                var product = this._context.Product.Where(c => c.Id == id).FirstOrDefault();
                if (product != null)
                {
                    this._context.Product.Remove(product);
                    this._context.SaveChanges();
                }
                else
                {
                    throw new Exception("Product doesn't exist");
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public Product Update(Product product)
        {
            try
            {
                this._context.Product.Update(product);
                this._context.SaveChanges();
                return product;
            }
            catch (Exception err)
            {
                throw;
            }
        }
    }
}

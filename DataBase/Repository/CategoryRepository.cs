using DataBase.Context;
using DataBase.Repository.Base;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Repository
{
    public class CategoryRepository : BaseRepository<CoreDbContext>, ICategoryRepository
    {
        public CategoryRepository(CoreDbContext context) : base(context)
        {
        }

        public int AddCategory(Category request)
        {
            try
            {
                _context.Category.Add(request);
                _context.SaveChanges();
                return request.Id;
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public Category GetCategoryById(int id)
        {
            try
            {
                Category category = this._context.Category.Where(c => c.Id == id).FirstOrDefault();
                if (category != null)
                {
                    return category;
                }
                else
                {
                    throw new Exception("Category doesn't exist");
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public List<Category> GetAllCategories()
        {
            try
            {
                var categories = _context.Category.ToList();
                return categories;
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public IQueryable<Category> GetCategorys(List<int> requestList = null)
        {
            var response = this._context.Category.AsNoTracking();
            if (requestList != null && requestList.Count() > 0)
            {
                response = this._context.Category.Where(c=> requestList.Contains(c.Id)).AsNoTracking();
            }
            return response;
        }

        public Category Update(Category category)
        {
            try
            {
                this._context.Category.Update(category);
                this._context.SaveChanges();
                return category;
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
                var category = this._context.Category.Where(c => c.Id == id).FirstOrDefault();
                if (category != null)
                {
                    this._context.Category.Remove(category);
                    this._context.SaveChanges();
                }
                else
                {
                    throw new Exception("Category doesn't exist");
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }
    }
}

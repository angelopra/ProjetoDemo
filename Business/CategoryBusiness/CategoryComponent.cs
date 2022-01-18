using Business.Base;
using DataBase.Context;
using DataBase.Repository;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Model.Request;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.CategoryBusiness
{
    public class CategoryComponent : BaseBusiness<ICategoryRepository>, ICategoryComponent
    {
        private readonly IValidator<CategoryRequest> _validator;
        public CategoryComponent(ICategoryRepository context, IValidator<CategoryRequest> validator) : base(context)
        {
            _validator = validator;
        }

        public int AddCategory(CategoryRequest request)
        {
            try
            {
                if (!_validator.Validate(request).IsValid)
                {
                    throw new Exception("sus");
                }
                var response = 0;

                var obj = new Category();
                obj.Name = request.Name;
                obj.Active = request.Active;

                if (String.IsNullOrEmpty(obj.Name) || String.IsNullOrWhiteSpace(obj.Name))
                {
                    throw new Exception("Insert a name");
                }

                response = this._context.AddCategory(obj);
                return response;
            }
            catch
            {
                throw;
            }
        }

        public Category Update(CategoryRequest request, int id)
        {
            try
            {
                if (!_validator.Validate(request).IsValid)
                {
                    throw new Exception("sus");
                }
                Category response = null;

                if (String.IsNullOrEmpty(request.Name) || String.IsNullOrWhiteSpace(request.Name))
                {
                    throw new Exception("Insert a name");
                }

                var obj = new Category();
                obj.Id = id;
                obj.Name = request.Name;
                obj.Active = request.Active;

                response = this._context.Update(obj);
                return response;
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
                this._context.Remove(id);
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
                var response = this._context.GetCategoryById(id);
                return response;
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public List<Category> GetCategorys(CategoryQueryRequest request)
        {
            try
            {
                var response = this._context.GetCategorys(request.IdCategories).ToList();
                return response;
            }
            catch (Exception err)
            {
                throw err;
            }
        }
    }
}

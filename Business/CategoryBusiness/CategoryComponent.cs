using Business.Base;
using DataBase.Context;
using DataBase.Repository;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Model.Request;
using Domain.Validators;
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
        private List<ValidateError> errors = null;
        public CategoryComponent(ICategoryRepository context, IValidator<CategoryRequest> validator) : base(context)
        {
            _validator = validator;
        }

        public int AddCategory(CategoryRequest request)
        {
            try
            {
                errors = ValidadeCategoryRequest(request);
                if (errors != null)
                {
                    throw new Exception();
                }

                var response = 0;
                var obj = MappingEntity<Category>(request);

                response = this._context.AddCategory(obj);
                return response;
            }
            catch (Exception err)
            {
                MapperException(err, errors);
                throw;
            }
        }

        public Category Update(CategoryRequest request, int id)
        {
            try
            {
                errors = ValidadeCategoryRequest(request);
                if (errors != null)
                {
                    throw new Exception();
                }

                Category response = null;

                if (String.IsNullOrEmpty(request.Name) || String.IsNullOrWhiteSpace(request.Name))
                {
                    throw new Exception("Insert a name");
                }

                var obj = MappingEntity<Category>(request);
                obj.Id = id;

                response = _context.Update(obj);
                return response;
            }
            catch (Exception err)
            {
                MapperException(err, errors);
                throw;
            }
        }

        public void Remove(int id)
        {
            try
            {
                _context.Remove(id);
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
                var response = _context.GetCategoryById(id);
                return response;
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
                var response = _context.GetAllCategories();
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
                var response = _context.GetCategorys(request.IdCategories).ToList();
                return response;
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        private List<ValidateError> ValidadeCategoryRequest(CategoryRequest request)
        {
            errors = null;
            var validate = _validator.Validate(request);
            if (!validate.IsValid)
            {
                errors = new List<ValidateError>();
                foreach (var failure in validate.Errors)
                {
                    var error = new ValidateError();
                    error.PropertyName = failure.PropertyName;
                    error.Error = failure.ErrorMessage;
                    errors.Add(error);
                }
            }
            return errors;
        }
    }
}

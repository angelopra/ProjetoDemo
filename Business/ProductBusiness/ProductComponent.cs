using Business.Base;
using DataBase.Context;
using DataBase.Repository;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Model.Request;
using Domain.Model.Response;
using Domain.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ProductBusiness
{
    public class ProductComponent : BaseBusiness<IProductRepository>, IProductComponent
    {
        private ICategoryComponent _categoryComponent;
        private readonly IValidator<ProductRequest> _validator;
        private List<ValidateError> errors;
        public ProductComponent(IProductRepository context, ICategoryComponent categoryComponent, IValidator<ProductRequest> validator)
            : base(context)
        {
            _categoryComponent = categoryComponent;
            _validator = validator;
        }

        public int AddProduct(ProductRequest request)
        {
            try
            {
                errors = ValidadeProductRequest(request);
                if (errors != null)
                {
                    throw new Exception();
                }

                var response = 0;

                var obj = request.Map<Product>();

                var category = _categoryComponent.GetCategoryById(request.IdCategory);
                if(category == null)
                {
                    throw new Exception("Category does not exist");
                }
                obj.Category = category;

                response = _context.AddProduct(obj);
                return response;
            }
            catch (Exception err)
            {
                MapperException(err, errors);
                throw;
            }
        }

        public Product GetProductById(int id)
        {
            try
            {
                var response = _context.GetProductById(id);
                return response;
            }
            catch
            {
                throw;
            }
        }

        public List<ProductListResponse> GetProductsByCategoryId(int categoryId, int? pageNumber, int? pageSize)
        {
            try
            {
                var response = _context.GetProductsByCategoryId(categoryId)
                    .Paginate(pageNumber, pageSize).Map<List<ProductListResponse>>();
                return response;
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
                _context.Remove(id);
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public Product Update(ProductRequest request, int id)
        {
            try
            {
                errors = ValidadeProductRequest(request);
                if (errors != null)
                {
                    throw new Exception();
                }

                var obj = request.Map<Product>();
                obj.Id = id;

                var category = _categoryComponent.GetCategoryById(request.IdCategory);
                if (category == null)
                {
                    throw new Exception("Category does not exist");
                }
                obj.Category = category;

                var response = _context.Update(obj);
                return response;
            }
            catch (Exception err)
            {
                MapperException(err, errors);
                throw;
            }
        }

        private List<ValidateError> ValidadeProductRequest(ProductRequest request)
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

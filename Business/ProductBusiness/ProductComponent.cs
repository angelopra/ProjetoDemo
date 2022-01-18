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

namespace Business.ProductBusiness
{
    public class ProductComponent : BaseBusiness<IProductRepository>, IProductComponent
    {
        private ICategoryComponent _categoryComponent;
        private readonly IValidator<ProductRequest> _validator;
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
                if (!_validator.Validate(request).IsValid)
                {
                    throw new Exception("sus");
                }
                var response = 0;

                var obj = MappingEntity<Product>(request);

                if (String.IsNullOrEmpty(obj.Name) || String.IsNullOrWhiteSpace(obj.Name))
                {
                    throw new Exception("Insert a name");
                }

                var category = _categoryComponent.GetCategoryById(request.IdCategory);
                if(category != null)
                {
                    obj.Category = category; // pq que aqui é um objeto category e não só o ID da categoria?
                }

                response = this._context.AddProduct(obj);
                return response;
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
                var response = this._context.GetProductById(id);
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
                this._context.Remove(id);
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
                if (!_validator.Validate(request).IsValid)
                {
                    throw new Exception("sus");
                }
                Product response;

                var obj = new Product();
                obj.Id = id;
                obj.Name = request.Name;
                obj.Description = request.Description;
                obj.Price = request.Price;
                obj.Quantity = request.Quantity;
                obj.Active = request.Active;

                var category = _categoryComponent.GetCategoryById(request.IdCategory);
                if (category != null)
                {
                    obj.Category = category;
                }

                response = _context.Update(obj);
                return response;
            }
            catch
            {
                throw;
            }
        }
    }


}

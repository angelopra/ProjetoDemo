using Business.Base;
using DataBase.Context;
using DataBase.Repository;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Model.Request;
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
        public ProductComponent(IProductRepository context, ICategoryComponent categoryComponent) : base(context)
        {
            _categoryComponent = categoryComponent;
        }

        public int AddProduct(ProductRequest request)
        {
            try
            {
                var response = 0;

                var obj = new Product();
                obj.Name = request.Name;
                obj.Description = request.Description;
                obj.Price = request.Price;
                obj.Quantity = request.Quantity;
                obj.Active = request.Active;

                if (String.IsNullOrEmpty(obj.Name) || String.IsNullOrWhiteSpace(obj.Name))
                {
                    throw new Exception("Insert a name");
                }

                var category = _categoryComponent.GetCategoryById(request.IdCategory);
                if(category != null)
                {
                    obj.Category = category;
                }

                response = this._context.AddProduct(obj);
                return response;
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
            catch (Exception err)
            {
                throw err;
            }
        }
    }


}

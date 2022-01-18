using System;
using Business.Base;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Model.Request;
using Domain.Entities.Base;
using Domain.Entities;
using FluentValidation;

namespace Business.CartBusiness
{
    public class CartComponent : BaseBusiness<ICartRepository>, ICartComponent
    {
        private readonly IValidator<CartRequest> _validator;
        public CartComponent(ICartRepository context, IValidator<CartRequest> validator) : base(context)
        {
            _validator = validator;
        }

        public int AddCart(CartRequest request)
        {
            try
            {
                if (!_validator.Validate(request).IsValid)
                {
                    throw new Exception("sus");
                }

                var response = 0;

                var obj = new Cart();
                obj.Active = request.Active;
                obj.IdCustomer = request.IdCustomer;

                response = this._context.AddCart(obj);
                return response;
            }
            catch
            {
                throw;
            }
        }

        public Cart GetCartById(int id)
        {
            try
            {
                var response = this._context.GetCartById(id);
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

        public int RemoveAllItems(int id)
        {
            try
            {
                var numberDeleted = _context.RemoveAllItems(id);
                var cart = _context.GetCartById(id);
                cart.Total = 0;
                _context.Update(cart);

                return numberDeleted;
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public Cart Update(CartRequest request, int id)
        {
            try
            {
                if (!_validator.Validate(request).IsValid)
                {
                    throw new Exception("sus");
                }

                Cart response;

                var obj = new Cart();
                obj.Id = id;
                obj.Active = request.Active;
                obj.IdCustomer = request.IdCustomer;

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

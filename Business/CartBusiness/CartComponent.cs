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

namespace Business.CartBusiness
{
    public class CartComponent : BaseBusiness<ICartRepository>, ICartComponent
    {
        public CartComponent(ICartRepository context) : base(context)
        {
        }

        public int AddCart(CartRequest request)
        {
            try
            {
                var response = 0;

                var obj = new Cart();
                obj.Active = request.Active;
                obj.IdCustomer = request.IdCustomer;

                response = this._context.AddCart(obj);
                return response;
            }
            catch (Exception err)
            {
                throw err;
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
                Cart response;

                var obj = new Cart();
                obj.Id = id;
                obj.Active = request.Active;
                obj.IdCustomer = request.IdCustomer;

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

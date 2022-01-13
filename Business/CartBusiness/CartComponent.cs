using System;
using Business.Base;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Model.Request;
using Domain.Entities.Base;

namespace Business.CartBusiness
{
    internal class CartComponent : BaseBusiness<ICartRepository>, ICartComponent
    {
        private ICartComponent _cartComponent;
        protected CartComponent(ICartRepository context, ICartComponent cartComponent) : base(context)
        {
            _component = cartComponent;
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
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public Cart GetCartById(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public Cart Update(CartRequest request, int id)
        {
            throw new NotImplementedException();
        }
    }
}

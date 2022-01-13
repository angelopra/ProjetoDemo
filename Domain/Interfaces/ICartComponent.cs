using System;
using System.Collections.Generic;
using Domain.Model.Request;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Base;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface ICartComponent
    {
        int AddCart(CartRequest request);
        Cart Update(CartRequest request, int id);
        void Remove(int id);
        Cart GetCartById(int id);
    }
}

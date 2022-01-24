using System;
using System.Collections.Generic;
using Domain.Model.Request;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Base;
using Domain.Entities;
using Domain.Model.Response;

namespace Domain.Interfaces
{
    public interface ICartComponent
    {
        int AddCart(CartRequest request);
        CartResponse Update(CartRequest request, int id);
        void Remove(int id);
        CartResponse GetCartById(int id);
        int RemoveAllItems(int id);
    }
}

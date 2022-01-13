using Domain.Entities;
using Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ICartRepository
    {
        int AddCart(Cart request);

        Cart GetCartById(int id);

        Cart Update(Cart request);

        void Remove(int id);
    }
}

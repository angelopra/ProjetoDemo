using Domain.Entities;
using Domain.Model.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IProductComponent
    {
        int AddProduct(ProductRequest request);
        Product GetProductById(int id);
        Product Update(ProductRequest request, int id);
        void Remove(int id);
    }
}

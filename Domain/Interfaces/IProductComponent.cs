using Domain.Entities;
using Domain.Model.Request;
using Domain.Model.Response;
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
        List<ProductListResponse> GetProductsByCategoryId(int categoryId, int? pageNumber, int? pageSize);
        Product Update(ProductRequest request, int id);
        void Remove(int id);
    }
}

using Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Interfaces
{
    public interface IProductRepository
    {
        int AddProduct(Product request);

        Product GetProductById(int id);

        public IQueryable<Product> GetProductsByCategoryId(int categoryId);

        Product Update(Product request);

        void Remove(int id);
    }
}

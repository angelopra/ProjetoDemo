using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IProductRepository
    {
        int AddProduct(Product request);

        Product GetProductById(int id);

        void Remove(int id);
    }
}

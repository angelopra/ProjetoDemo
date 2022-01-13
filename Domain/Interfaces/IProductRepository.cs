using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IProductRepository
    {
        int AddProduct(Product request);

        Product GetProductById(int id);

        Product Update(Product request);

        void Remove(int id);
    }
}

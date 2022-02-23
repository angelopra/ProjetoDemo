using Domain.Entities;

namespace Domain.Interfaces
{
    public interface ICartBusinessMethods
    {
        Cart GetCartById(int id);
        bool CartItemExists(int idCart, int idProduct);
        CartItem GetCartItem(int idCart, int idProduct);
    }
}

using Domain.Entities;

namespace Domain.Interfaces
{
    public interface ICartBusinessMethods
    {
        bool CartItemExists(int idCart, int idProduct);
        Cart GetCart(int id);
        CartItem GetCartItem(int idCart, int idProduct);
    }
}

using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Domain.Interfaces
{
    public interface IUnityOfWorkBase
    {
        DbSet<Product> Product { get; set; }
        DbSet<Category> Category { get; set; }
        DbSet<Customer> Customer { get; set; }
        DbSet<Cart> Cart { get; set; }
        DbSet<CartItem> CartItem { get; set; }
        DbSet<Order> Order { get; set; }
    }
}

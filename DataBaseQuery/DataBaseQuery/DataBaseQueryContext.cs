using DataBaseQuery.Configuration;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataBase.DataBaseQuery
{
    public class DataBaseQueryContext : DbContext, IUnityOfWorkQuery
    {
        public DataBaseQueryContext(DbContextOptions<DataBaseQueryContext> options) : base(options)
        {
        }

        public DbSet<Product> Product { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Cart> Cart { get; set; }
        public DbSet<CartItem> CartItem { get; set; }
        public DbSet<Order> Order { get; set; }

        public async Task<int> Commit(CancellationToken cancellationToken = new CancellationToken())
        {
            try
            {
                var result = await base.SaveChangesAsync(cancellationToken);
                return result;
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataBaseQueryContext).Assembly);
            var entities = modelBuilder.Model.GetEntityTypes();

            var foreignKeys = entities
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in foreignKeys)
                fk.DeleteBehavior = DeleteBehavior.Restrict;

            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}

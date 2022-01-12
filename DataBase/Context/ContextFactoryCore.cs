using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Context
{
    public class ContextFactoryCore : IDesignTimeDbContextFactory<CoreDbContext>
    {
        public CoreDbContext CreateDbContext(string[] args)
        {
            var parentDirectory = Directory.GetParent(Directory.GetCurrentDirectory());
            var directories = parentDirectory.GetDirectories("ProjetoDemo");
            string apiDirectory = directories[0].FullName;

            var builderConfiguration = new ConfigurationBuilder()
                .SetBasePath(apiDirectory)
                .AddJsonFile("appsettings.json");
            var configuration = builderConfiguration.Build();
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            var builder = new DbContextOptionsBuilder<CoreDbContext>();
            builder.UseSqlServer(connectionString);

            return new CoreDbContext(builder.Options);
        }
    }
}

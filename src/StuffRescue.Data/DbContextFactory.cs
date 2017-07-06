using Core.Common.Configuration;
using Core.Common.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Reflection;

namespace StuffRescue.Data
{
    public class DbContextFactory : IDbContextFactory<StuffRescueContext>
    {
        public StuffRescueContext Create(DbContextFactoryOptions options)
        {
            var builder = new DbContextOptionsBuilder<StuffRescueContext>();
            builder.UseSqlServer(ConfigHelper.ConnectionStrings.Value);//,
                //optionsBuilder => optionsBuilder.MigrationsAssembly(typeof(StuffRescueContext).GetTypeInfo().Assembly.GetName().Name));
            return new StuffRescueContext(builder.Options);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Core.Common.Data
{
    public interface IDbContextFactoryBase<T>: IDbContextFactory<T> 
        where T : DbContext, new()
    {
    }
}

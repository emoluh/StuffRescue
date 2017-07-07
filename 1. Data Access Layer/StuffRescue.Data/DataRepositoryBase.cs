using Core.Common.Data;

namespace StuffRescue.Data
{
    public abstract class DataRepositoryBase<T> : DataRepositoryBase<T, StuffRescueContext>
       where T : class, new()
    {
    }
}

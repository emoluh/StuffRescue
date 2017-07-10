using System;
using Core.Common.Contracts;

namespace StuffRescue.Data
{
    public class DataRepositoryFactory : IDataRepositoryFactory
    {
        public T GetDataRepository<T>() where T : IDataRepository
        {
            throw new NotImplementedException();
        }
    }
}

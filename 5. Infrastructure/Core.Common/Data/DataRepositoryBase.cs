using Core.Common.Contracts;
using Core.Common.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;

namespace Core.Common.Data
{
    public abstract class DataRepositoryBase<T, U> : IDataRepository<T>
          where T : class, new()
          where U : DbContext, new()
    {
            protected abstract T AddEntity(U entityContext, T entity);
            protected abstract T UpdateEntity(U entityContext, T entity);
            protected abstract IEnumerable<T> GetEntities(U entityContext);
            protected abstract T GetEntity(U entityContext, int i);
            public T Add(T entity)
            {
                using (U entityContext = new U())
                {
                    T addedEntity = AddEntity(entityContext, entity);
                    entityContext.SaveChanges();
                    return addedEntity;
                }
            }
            public void Remove(T entity)
            {
                using (U entityContext = new U())
                {
                    entityContext.Entry<T>(entity).State = EntityState.Deleted;
                    entityContext.SaveChanges();
                }
            }
            public void Remove(int id)
            {
                using (U entityContext = new U())
                {
                    T entity = GetEntity(entityContext, id);
                    entityContext.Entry<T>(entity).State = EntityState.Deleted;
                    entityContext.SaveChanges();
                }
            }
            public T Update(T entity)
            {
                using (U entityContext = new U())
                {
                    T existingEntity = UpdateEntity(entityContext, entity);
                    SimpleMapper.PropertyMap(entity, existingEntity);
                    entityContext.SaveChanges();
                    return existingEntity;
                }
            }
            public IEnumerable<T> Get()
            {
                //TODO : Fix the DI Container Dispose Issue
                //using (U entityContext = new U())
                U entityContext = new U();
                    return GetEntities(entityContext);
            }
            public T Get(int id)
            {
                using (U entityContext = new U())
                    return GetEntity(entityContext, id);
            }
        }
}

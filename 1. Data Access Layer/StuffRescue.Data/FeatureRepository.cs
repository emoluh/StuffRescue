using System.Linq;
using System.Collections.Generic;
using StuffRescue.Business.Entities;
using StuffRescue.Data.Contracts;

namespace StuffRescue.Data
{
    public class FeatureRepository : DataRepositoryBase<Feature>, IFeatureRepository
    {
        protected override Feature AddEntity(StuffRescueContext entityContext, Feature entity)
        {
            return entityContext.FeatureSet.Add(entity).Entity;
        }

        protected override IEnumerable<Feature> GetEntities(StuffRescueContext entityContext)
        {
            return from e in entityContext.FeatureSet
                   select e;
        }

        protected override Feature GetEntity(StuffRescueContext entityContext, int id)
        {
            var query = (from e in entityContext.FeatureSet
                         where e.FeatureId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        protected override Feature UpdateEntity(StuffRescueContext entityContext, Feature entity)
        {
            return (from e in entityContext.FeatureSet
                    where e.FeatureId == entity.FeatureId
                    select e).FirstOrDefault();
        }
    }
}

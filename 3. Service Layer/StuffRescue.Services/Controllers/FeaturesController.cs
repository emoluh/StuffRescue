using Core.Common.Contracts;
using Microsoft.AspNetCore.Mvc;
using StuffRescue.Business.Entities;
using StuffRescue.Data.Contracts;
using StuffRescue.Services.Mapping;
using StuffRescue.Services.Messaging.FeaturesService;
using System.Collections.Generic;
using System.Linq;

namespace StuffRescue.Services.Controllers
{
    [Route("api/[controller]")]
    public class FeaturesController : Controller
    {
        IDataRepositoryFactory _dataRepositoryFactory;

        public FeaturesController(IDataRepositoryFactory dataRepositoryFactory)
        {
            _dataRepositoryFactory = dataRepositoryFactory;
        }

        // GET: api/values
        [HttpGet]
        public IActionResult Get()
        {
            GetAllFeaturesResponse response = new GetAllFeaturesResponse();
            IFeatureRepository featureRepository
                    = _dataRepositoryFactory.GetDataRepository<IFeatureRepository>();

            IEnumerable<Feature> features = featureRepository.Get();

            response.Features = features.ConvertToFeatureViewModel();

            return Ok(response.Features);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

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
        IFeatureRepository _featureRepository;

        public FeaturesController(IDataRepositoryFactory dataRepositoryFactory)
        {
            _dataRepositoryFactory = dataRepositoryFactory;
            _featureRepository 
                = _dataRepositoryFactory.GetDataRepository<IFeatureRepository>();
        }

        // GET: api/values
        [HttpGet]
        public IActionResult Get()
        {
            GetAllFeaturesResponse response = new GetAllFeaturesResponse();

            IEnumerable<Feature> features = _featureRepository.Get();

            response.Features = features.ConvertToFeatureViewModel();

            return Ok(response.Features);
        }

        // GET api/values/5
        //TODO Use GetFeaureRequest Object From Body
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            GetFeatureResponse response = new GetFeatureResponse();

            Feature feature = _featureRepository.Get(id);

            response.Feature = feature.ConvertToFeatureDetailViewModel();

            return Ok(response.Feature);
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

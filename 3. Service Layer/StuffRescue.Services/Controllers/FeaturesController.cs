using Core.Common.Contracts;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using StuffRescue.Business.Entities;
using StuffRescue.Data.Contracts;
using StuffRescue.Services.Mapping;
using StuffRescue.Services.Messaging.FeaturesService;
using StuffRescue.Services.Models.FeaturesToggleViewModels;
using System;
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

        // GET: api/features
        [HttpGet]
        public IActionResult Get()
        {
            GetAllFeaturesResponse response = new GetAllFeaturesResponse();

            IEnumerable<Feature> features = _featureRepository.Get();

            response.Features = features.ConvertToFeatureViewModel();

            return Ok(response.Features);
        }

        // GET api/features/5
        //TODO Use GetFeaureRequest Object From Body
        [HttpGet("{id}", Name = "FeatureLookup")]
        public IActionResult Get(int id)
        {
            GetFeatureResponse response = new GetFeatureResponse();

            Feature feature = _featureRepository.Get(id);

            response.Feature = feature.ConvertToFeatureDetailViewModel();

            return Ok(response.Feature);
        }

        // POST api/features
        [HttpPost]
        public IActionResult Post([FromBody]CreateFeatureRequest Request)
        {
            CreateFeatureResponse response = new CreateFeatureResponse();
            Feature  addedEntity = new Feature();
            addedEntity.Name = Request.Name;
            addedEntity.Enabled = Request.Enabled;

            try
            {
                Feature addedFeature = _featureRepository.Add(addedEntity);

                var newUri = Url.Link("FeatureLookup", new { id = addedFeature.FeatureId });

                response.Feature = addedFeature.ConvertToFeatureDetailViewModel();

                return Created(newUri, response.Feature);

            }
            catch (Exception ex)
            {
                //Log Exception
            }

            return BadRequest("Failed to save the feature");
        }

        // PUT api/features/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]UpdateFeatureRequest Request)
        {
            Feature feature = _featureRepository.Get(id);

            if (feature == null)
            {
                return NotFound($"Feature {id} does not exist");
            }

            UpdateFeatureResponse response = new UpdateFeatureResponse();
            Feature updatedEntity = new Feature();
            updatedEntity.FeatureId = id;
            updatedEntity.Name = Request.Name;
            updatedEntity.Enabled = Request.Enabled;
            try
            {
                Feature updatedFeature = _featureRepository.Update(updatedEntity);

                response.Feature = updatedFeature.ConvertToFeatureDetailViewModel();

                return Ok(response.Feature);
            }
            catch (Exception)
            {
                //Log Exception
            }
            return BadRequest($"Failed to delete feature {id}");
        }

        // DELETE api/features/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Feature feature = _featureRepository.Get(id);

            if(feature == null)
            {
                return NotFound($"Feature {id} does not exist");
            }
            try
            {
                _featureRepository.Remove(id);

                return Ok();
            }
            catch (Exception)
            {
                //Log Exception
            }
            return BadRequest($"Failed to delete feature {id}");

        }
    }
}

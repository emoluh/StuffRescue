﻿using Core.Common.Contracts;
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
        [HttpGet("{id}", Name = "FeatureLookup")]
        public IActionResult Get(int id)
        {
            GetFeatureResponse response = new GetFeatureResponse();

            Feature feature = _featureRepository.Get(id);

            response.Feature = feature.ConvertToFeatureDetailViewModel();

            return Ok(response.Feature);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]CreateFeatureRequest Request)
        {
            Feature  addedEntity = new Feature();
            addedEntity.Name = Request.Name;
            addedEntity.Enabled = Request.Enabled;

            try
            {
                Feature addedFeature = _featureRepository.Add(addedEntity);

                var newUri = Url.Link("FeatureLookup", new { id = addedFeature.FeatureId });

                return Created(newUri, addedFeature);

            }
            catch (Exception ex)
            {
                //Log Error
            }

            return BadRequest("Failed to save the feature");
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

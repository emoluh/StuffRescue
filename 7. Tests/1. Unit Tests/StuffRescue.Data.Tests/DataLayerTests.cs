using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using StuffRescue.Business.Bootstrapper;
using StuffRescue.Business.Entities;
using StuffRescue.Data.Contracts;
using System.Collections.Generic;
using Xunit;

namespace StuffRescue.Data.Tests
{
    public class DataLayerTests
    {
        public IConfigurationRoot Configuration;

        public IServiceCollection Services;

        public DataLayerTests()
        {
           //Services.Init(Configuration);
        }

        //[Fact]
        public void test_repository_usage()
        {
            RepositoryTestClass repositoryTest = new RepositoryTestClass();

            IEnumerable<Feature> features = repositoryTest.GetFeatures();

            Assert.NotNull(features);
        }

        [Fact]
        public void test_repository_mocking()
        {
            List<Feature> features = new List<Feature>
            {
                new Feature(){ FeatureId = 1, Name = "Facebook", Enabled = true },
                new Feature(){ FeatureId = 2, Name = "Google", Enabled = false}
            };

            Mock<IFeatureRepository> mockFeatureRepository = new Mock<IFeatureRepository>();

            mockFeatureRepository.Setup(obj => obj.Get()).Returns(features);

            RepositoryTestClass repositoryTest = new RepositoryTestClass(mockFeatureRepository.Object);

            IEnumerable<Feature> ret = repositoryTest.GetFeatures();

            Assert.Equal(features, ret);
        }

    }
    public class RepositoryTestClass
    {
        public RepositoryTestClass()
        {

        }
        public RepositoryTestClass(IFeatureRepository FeatureRepository)
        {
            _FeatureRepository = FeatureRepository;
        }

        IFeatureRepository _FeatureRepository;

        public IEnumerable<Feature> GetFeatures()
        {
            IEnumerable<Feature> features = _FeatureRepository.Get();

            return features;
        }
    }
}

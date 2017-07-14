using Core.Common.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using StuffRescue.Business.Bootstrapper;
using StuffRescue.Business.Entities;
using StuffRescue.Data.Common;
using StuffRescue.Data.Contracts;
using System;
using System.Collections.Generic;
using Xunit;

namespace StuffRescue.Data.Tests
{
    public class DataLayerTests
    {
        [Fact]
        public void test_repository_usage()
        {
            RepositoryTestClass repositoryTest = new RepositoryTestClass();

            IEnumerable<Feature> features = repositoryTest.GetFeatures();

            Assert.NotNull(features);
        }

        [Fact]
        public void test_repository_factory_usage()
        {
            RepositoryFactoryTestClass factoryTest = new RepositoryFactoryTestClass();

            IEnumerable<Feature> features = factoryTest.GetFeatures();

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

        [Fact]
        public void test_factory_mocking1()
        {
            List<Feature> features = new List<Feature>
            {
                new Feature(){ FeatureId = 1, Name = "Facebook", Enabled = true },
                new Feature(){ FeatureId = 2, Name = "Google", Enabled = false}
            };

            Mock<IDataRepositoryFactory> mockDataRepository = new Mock<IDataRepositoryFactory>();

            mockDataRepository.Setup(obj => obj.GetDataRepository<IFeatureRepository>().Get()).Returns(features);

            RepositoryFactoryTestClass factoryTest = new RepositoryFactoryTestClass(mockDataRepository.Object);

            IEnumerable<Feature> ret = factoryTest.GetFeatures();

            Assert.Equal(features, ret);
        }

        [Fact]
        public void test_factory_mocking2()
        {
            List<Feature> features = new List<Feature>
            {
                new Feature(){ FeatureId = 1, Name = "Facebook", Enabled = true },
                new Feature(){ FeatureId = 2, Name = "Google", Enabled = false}
            };


            Mock<IFeatureRepository> mockFeatureRepository = new Mock<IFeatureRepository>();

            mockFeatureRepository.Setup(obj => obj.Get()).Returns(features);


            Mock<IDataRepositoryFactory> mockDataRepository = new Mock<IDataRepositoryFactory>();

            mockDataRepository.Setup(obj => obj.GetDataRepository<IFeatureRepository>()).Returns(mockFeatureRepository.Object);


            RepositoryFactoryTestClass factoryTest = new RepositoryFactoryTestClass(mockDataRepository.Object);

            IEnumerable<Feature> ret = factoryTest.GetFeatures();

            Assert.Equal(features, ret);
        }

    }

    /// <summary>
    /// Simulates a service where it is exposing the service operation called GetFeatures
    /// or business engine whose job is to get a Features , acts against that collection 
    /// before it sends it to the top either way it a client to the FeatureRepository
    /// </summary>
    public class RepositoryTestClass
    {
        public IConfigurationRoot Configuration;

        public IServiceCollection Services;

        public RepositoryTestClass()
        {
            Services = new ServiceCollection();

            var builder = new ConfigurationBuilder();

            IConfigurationRoot Configuration = builder.Build();

            Services = Services.Init(Configuration);

            // create service provider
            IServiceProvider provider = Services.BuildServiceProvider();

            _FeatureRepository =  provider.GetService<IFeatureRepository>();
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

    public class RepositoryFactoryTestClass
    {
        public IConfigurationRoot Configuration;

        public IServiceCollection Services;
        public RepositoryFactoryTestClass()
        {

            Services = new ServiceCollection();

            var builder = new ConfigurationBuilder();

            IConfigurationRoot Configuration = builder.Build();

            Services = Services.Init(Configuration);

            Services.AddTransient<IDataRepositoryFactory, DataRepositoryFactory>();

            // create service provider
            IServiceProvider provider = Services.BuildServiceProvider();

            _DataRepositoryFactory = provider.GetService<IDataRepositoryFactory>();
        }

        public RepositoryFactoryTestClass(IDataRepositoryFactory DataRepositoryFactory)
        {
            _DataRepositoryFactory = DataRepositoryFactory;
        }

        IDataRepositoryFactory _DataRepositoryFactory;

        public IEnumerable<Feature> GetFeatures()
        {
            IFeatureRepository featureRepository = _DataRepositoryFactory.GetDataRepository<IFeatureRepository>();

            IEnumerable<Feature> features = featureRepository.Get();

            return features;
        }
    }
}

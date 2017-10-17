using AutoMapper;
using Core.Common.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using StuffRescue.Business.Bootstrapper;
using StuffRescue.Business.Entities;
using StuffRescue.Data.Common;
using StuffRescue.Services.Models.FeaturesToggleViewModels;

namespace StuffRescue.Services
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();

            services.Init();

            services.AddTransient<IDataRepositoryFactory, DataRepositoryFactory>();
            // Add framework services.
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();


            // Shows UseCors with CorsPolicyBuilder.
            app.UseCors(builder =>
               builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader());

            Mapper.Initialize(config =>
            {
                config.CreateMap<FeatureViewModel, Feature>().ReverseMap();
            });

            app.UseMvc();
        }
    }
}

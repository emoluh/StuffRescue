using Core.Common.Configuration;
using Core.Common.Contracts;
using Core.Common.Messaging;
using Core.Common.Messaging.Email;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StuffRescue.Business.Entities;
using StuffRescue.Data;
using StuffRescue.Data.Contracts;
using StuffRescue.FeatureToggle.Internal;

namespace StuffRescue.Business.Bootstrapper
{
    public static class ServiceCollectionLoader
    {
        public static IServiceCollection Init(this IServiceCollection services, IConfigurationRoot Configuration)
        {
            // Set provider config so file is read from content root path
            var provider = new AppSettingsProvider { Configuration = Configuration };
            
            //services.AddSingleton(new Facebook ());
            //TODO Figure the proper DI for use in startup file ApplicationSettingsFactory.GetApplicationSettings().ConnectionString
            //services.AddTransient<IApplicationSettings, WebConfigApplicationSettings>();

            services.AddSingleton(new Facebook { ToggleValueProvider = provider });

            services.AddTransient<IFeatureRepository, FeatureRepository>();

            // Add framework services.
            services.AddDbContext<StuffRescueContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString(ConfigHelper.ConnectionStrings.DefaultConnection)));

            services.AddIdentity<StuffRescueUser, IdentityRole>(config =>
            {
                config.SignIn.RequireConfirmedEmail = true;
            })
                .AddEntityFrameworkStores<StuffRescueContext>()
                .AddDefaultTokenProviders();



            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();


            services.Configure<AuthMessageSenderOptions>(config =>
            {
                config.SendGridUser = Configuration[ConfigHelper.Email.SendGrid.SendGridUser];
                config.SendGridKey = Configuration[ConfigHelper.Email.SendGrid.SendGridKey];
            });

            return services;
        }
    }
}

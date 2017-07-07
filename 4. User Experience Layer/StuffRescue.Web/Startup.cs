﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using StuffRescue.Web.Services;
using Microsoft.AspNetCore.Mvc;
using StuffRescue.FeatureToggle.Internal;
using StuffRescue.Web.Models.FeatureToggle;
using Core.Common.Configuration;
using StuffRescue.Business.Entities;
using StuffRescue.Data;

namespace StuffRescue.Web
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets<Startup>();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Set provider config so file is read from content root path
            var provider = new AppSettingsProvider { Configuration = Configuration };

            services.AddSingleton(new Facebook { ToggleValueProvider = provider });

            //services.AddSingleton(new Facebook ());
            //TODO Figure the proper DI for use in startup file ApplicationSettingsFactory.GetApplicationSettings().ConnectionString
            //services.AddTransient<IApplicationSettings, WebConfigApplicationSettings>();

            // Add framework services.
            services.AddDbContext<StuffRescueContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString(ConfigHelper.ConnectionStrings.DefaultConnection)));

            services.AddIdentity<StuffRescueUser, IdentityRole>(config =>
                {
                    config.SignIn.RequireConfirmedEmail = true;
                })
                .AddEntityFrameworkStores<StuffRescueContext>()
                .AddDefaultTokenProviders();


            services.AddMvc(options =>
            {
                options.SslPort = 44321;
                options.Filters.Add(new RequireHttpsAttribute());
            });


            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();

            services.Configure<AuthMessageSenderOptions>(config => 
            {
                config.SendGridUser = Configuration[ConfigHelper.Email.SendGrid.SendGridUser];
                config.SendGridKey = Configuration[ConfigHelper.Email.SendGrid.SendGridKey];
            });

            services.AddSingleton(config => Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection(ConfigHelper.Logging.Name));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseIdentity();

            // Add external authentication middleware below. To configure them please see http://go.microsoft.com/fwlink/?LinkID=532715

            //TODO: Execute the following commands in the project working directory (<base dir>\StuffRescue\src\StuffRescue.Web) to store the Google secrets: 
            //dotnet user-secrets set Authentication:Google:ClientID <client_id>
            //dotnet user-secrets set Authentication:Google: ClientSecret < client - secret >
            app.UseGoogleAuthentication(new GoogleOptions()
            {
                ClientId = Configuration[ConfigHelper.Authentication.Google.ClientId],
                ClientSecret = Configuration[ConfigHelper.Authentication.Google.ClientSecret]
            });


            //TODO: Execute the following commands in the project working directory (<base dir>\StuffRescue\src\StuffRescue.Web) to store the Facebook secrets: 
            //dotnet user-secrets set Authentication:Facebook:AppId <app-Id>
            //dotnet user-secrets set Authentication:Facebook:AppSecret <app-secret>
            app.UseFacebookAuthentication(new FacebookOptions()
            {
                AppId = Configuration[ConfigHelper.Authentication.Facebook.AppId],
                AppSecret = Configuration[ConfigHelper.Authentication.Facebook.AppSecret]
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
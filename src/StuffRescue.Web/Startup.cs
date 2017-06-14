using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using StuffRescue.Web.Data;
using StuffRescue.Web.Models;
using StuffRescue.Web.Services;
using Microsoft.AspNetCore.Mvc;
using StuffRescue.FeatureToggle.Internal;
using StuffRescue.Web.Models.FeatureToggle;

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

            // Add framework services.
            services.AddDbContext<StuffRescueDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<StuffRescueUser, IdentityRole>()
                .AddEntityFrameworkStores<StuffRescueDbContext>()
                .AddDefaultTokenProviders();


            services.AddMvc(options =>
            {
                options.SslPort = 44321;
                options.Filters.Add(new RequireHttpsAttribute());
            });


            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
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
                ClientId = Configuration["Authentication:Google:ClientId"],
                ClientSecret = Configuration["Authentication:Google:ClientSecret"]
            });


            //TODO: Execute the following commands in the project working directory (<base dir>\StuffRescue\src\StuffRescue.Web) to store the Facebook secrets: 
            //dotnet user-secrets set Authentication:Facebook:AppId <app-Id>
            //dotnet user-secrets set Authentication:Facebook:AppSecret <app-secret>
            app.UseFacebookAuthentication(new FacebookOptions()
            {
                AppId = Configuration["Authentication:Facebook:AppId"],
                AppSecret = Configuration["Authentication:Facebook:AppSecret"]
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

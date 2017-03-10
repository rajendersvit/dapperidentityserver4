using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Lshp.OpenIDConnect.Util.ConfigManager;
using Lshp.OpenIDConnect.Service.IdentityService;
using Microsoft.AspNetCore.Identity;
using Lshp.OpenIDConnect.Model.Entities;
using Lshp.OpenIDConnect.Data.Repository;
using Microsoft.Extensions.Options;
using Lshp.OpenIDConnect.Data.Interface.Service;
using Lshp.OpenIDConnect.Service.OpenIDConnect;
using Lshp.OpenIDConnect.Service.AdminService;
using Lshp.OpenIDConnect.Service.Logger;
using Lshp.OpenIDConnect.Service.Middleware; 

namespace Host
{
    public class Startup
    {
        private readonly IHostingEnvironment environment;
        public IConfigurationRoot Configuration { get; }
        public Startup(IHostingEnvironment env)
        {
            environment = env;
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            //.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true); 

            builder.AddEnvironmentVariables();

            Configuration = builder.Build();
            string appsettingPath = System.IO.Path.Combine(env.ContentRootPath, "appsettings.json");
            Lshp.OpenIDConnect.Util.ConfigurationManager.GetInstance().Init(appsettingPath);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<ConfigEntry>(Configuration);
            var userPrincipalFactory = new IdentityUserPrincipalFactory();
            services.AddSingleton<IUserClaimsPrincipalFactory<User>>(userPrincipalFactory);
            //// We can replace with our own password encryption for timebeing we are using the default password encryption
            //services.AddSingleton<IPasswordHasher<User>, UserPasswordHasher<User>>();

            services.AddSingleton<IUserStore<User>>(provider =>
            {
                var options = provider.GetService<IOptions<ConfigEntry>>();
                var userStore = new IdentityUserStore(
                new UserLoginRepository(options),
                new UserRepository(options),
                new UserClaimRepository(options),
                new RoleRepository(options),
                new UserRoleRepository(options));
                return userStore;
            });

            services.AddSingleton<IRoleStore<Role>>(provider =>
            {
                var options = provider.GetService<IOptions<ConfigEntry>>();
                var roleStore = new IdentityRoleStore(new RoleRepository(options));
                return roleStore;
            });

            services.AddSingleton<IManageOpenIDClient, ManageOpenIDClient>();

            services.AddIdentity<User, Role>()
                .AddDefaultTokenProviders()
                .AddIdentityServerUserClaimsPrincipalFactory();

            services.AddIdentityServer()
                .AddSigningCredentialStore(environment)
                .AddConfigurationStore()
                .AddDapperIdentity<User>();

            ////Gzip Compression to reduce the size of the response
            services.AddResponseCompression();
            services.AddMvc();
            services.AddAntiforgery(options => options.HeaderName = "RequestValidation");

            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddSingleton<ISmsSender>(provider => {
                var options = provider.GetService<IOptions<ConfigEntry>>();
                return new AuthMessageSender(options);
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IServiceProvider serviceProvider)
        {
            app.UseForwardedHeaders(new ForwardedHeadersOptions()
            {
                ForwardedHeaders = Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.All
            });

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            loggerFactory.AddSQLLogger(serviceProvider);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            ////Gzip Compression to reduce the size of the response
            app.UseResponseCompression();

            app.UseIdentity();

            app.UseRequestLocalization();
            app.UseCustomHttpHeaders(env);

            //start the identity server 
            app.UseIdentityServerWithCustomMiddleware();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });


        }
    }
}

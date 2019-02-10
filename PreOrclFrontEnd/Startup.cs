using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using PreOrclFrontEnd.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using PreOrclFrontEnd.Utilidades;
using PreOrclFrontEnd.Extensions;
using PreOrclFrontEnd.Helpers;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using System.Net;

namespace PreOrclFrontEnd
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        public const string ObjectIdentifierType = "http://schemas.microsoft.com/identity/claims/objectidentifier";
        public const string TenantIdType = "http://schemas.microsoft.com/identity/claims/tenantid";

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
                
            });


            services.AddMvc().AddRazorPagesOptions(options =>
            {
                options.Conventions.AddPageRoute("/Auth/Index", "");
            });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme
                //sharedOptions =>
                //{
                //    sharedOptions.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                //    sharedOptions.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                //    sharedOptions.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;


                //}
                )
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,
                options =>
            {
                //options.Cookie.Name = "name";
                options.LoginPath = "/Auth";
            }
            )
            //.AddMicrosoftAccount(microsoftOptions =>
            //{
            //    microsoftOptions.ClientId = "c29c2d9c-8219-4b67-8012-7f8f1fb17947";
            //    microsoftOptions.ClientSecret = "cqlrcFPUMS72807)*)hqIN}";
            //    microsoftOptions.Events.OnRemoteFailure = (context) =>
            //    {
            //        context.Response.Redirect("/Auth");
            //        context.HandleResponse();
            //        return System.Threading.Tasks.Task.FromResult(0);
            //    };
            //})
            .AddAzureAd(options => Configuration.Bind("AzureAd", options));

            services.AddMemoryCache();
            services.AddSession();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
           
            // Add application services.
            //services.AddSingleton<IConfiguration>(Configuration);
            services.AddSingleton<IGraphAuthProvider, GraphAuthProvider>();
            services.AddTransient<IGraphSdkHelper, GraphSdkHelper>();

            services.Configure<HstsOptions>(options =>
            {
               
                options.IncludeSubDomains = true;
                options.MaxAge = TimeSpan.FromDays(365);
            });
           
            services.Configure<ConfigurationJson>(Configuration.GetSection("UriHelpers"));
            services.AddDbContext<PreOrclFrontEndContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("PreOrclFrontEndContext")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            

            app.UseHttpsRedirection();
            app.UseSession();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

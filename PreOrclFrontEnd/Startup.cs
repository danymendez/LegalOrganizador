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

namespace PreOrclFrontEnd
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
            services.AddMvc().AddRazorPagesOptions(options =>
            {
                options.Conventions.AddPageRoute("/SisPerPersonas/Index", "");
            });

            //services.Configure<CookiePolicyOptions>(options =>
            //{
            //    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
            //    options.CheckConsentNeeded = context => false;
            //    options.MinimumSameSitePolicy = SameSiteMode.None;
            //});

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(
                options =>
            {
                options.Cookie.Name = "usuario";

                options.LoginPath = "/Auth";
            }
            ).AddMicrosoftAccount(microsoftOptions =>
            {
                microsoftOptions.ClientId = "c29c2d9c-8219-4b67-8012-7f8f1fb17947";
                microsoftOptions.ClientSecret = "cqlrcFPUMS72807)*)hqIN}";
                microsoftOptions.Events.OnRemoteFailure = (context) =>
                {
                    context.Response.Redirect("/Auth");
                    context.HandleResponse();
                    return System.Threading.Tasks.Task.FromResult(0);
                };
            }); 

            //services.ConfigureApplicationCookie(options =>
            //{
            //    options.AccessDeniedPath = "/Auth/AccessDenied";
            //    options.Cookie.Name = "usuario";
            //    options.Cookie.HttpOnly = true;
            //    options.ExpireTimeSpan = TimeSpan.FromMinutes(720);
            //    options.LoginPath = "/Auth";
            //    options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
            //    options.SlidingExpiration = true;
            //});
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            // services.AddIdentity<IdentityUser, IdentityRole>(options => {

            // })
            //.AddEntityFrameworkStores<PreOrclFrontEndContext>();
            services.Configure<ConfigurationJson>(Configuration.GetSection("UriHelpers"));
            services.AddDbContext<PreOrclFrontEndContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("PreOrclFrontEndContext")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //app.UseCookieAuthentication(new CookieAuthenticationOptions
            //{
            //    AuthenticationScheme = "MyCookie",
            //    AutomaticAuthenticate = true,
            //    AutomaticChallenge = true,
            //    LoginPath = new PathString("/account/login")
            //});

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                //routes.MapRoute(
                //    name: "areaRoute",
                //    template: "{area:Identity}/{controller=Home}/{action=Index}/{id?}");
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

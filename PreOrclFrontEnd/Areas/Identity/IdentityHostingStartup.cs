using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PreOrclFrontEnd.Models;

[assembly: HostingStartup(typeof(PreOrclFrontEnd.Areas.Identity.IdentityHostingStartup))]
namespace PreOrclFrontEnd.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<PreOrclFrontEndDBContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("PreOrclFrontEndDBContextConnection")));

                services.AddDefaultIdentity<IdentityUser>()
                    .AddEntityFrameworkStores<PreOrclFrontEndDBContext>();
            });
        }
    }
}
using CsharpClicker.Domain;
using CsharpClicker.Infrastructure.DataAccess;
using Microsoft.AspNetCore.Identity;

namespace CsharpClicker.Initizlizers
{
    public static class IdentityInitializer
    {
        public static void AddIdentity(IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();
            services.Configure<IdentityOptions>(o => o.Password.RequireNonAlphanumeric = false);
        }
    }
}

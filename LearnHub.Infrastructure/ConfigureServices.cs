using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using LearnHub.Application.Common.Interfaces;
using LearnHub.Infrastructure.Identity;
using LearnHub.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using LearnHub.Infrastructure.Persistence.Configuration.Identity;

namespace LearnHub.Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
            services.AddScoped<IIdentityService,IdentityService>();

            services.AddDbContext<ApplicationDbContext>(option =>
            {
                option.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
                    builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));

            });

            services.AddDefaultIdentity<User>()
                .AddEntityFrameworkStores<ApplicationDbContext>().AddErrorDescriber<PersianIdentityErrorDescriber>();

            return services;
        }
    }
}

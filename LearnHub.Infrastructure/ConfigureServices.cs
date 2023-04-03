using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using LearnHub.Application.Common.Interfaces;
using LearnHub.Infrastructure.Identity;
using LearnHub.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using LearnHub.Infrastructure.Persistence.Configuration.Identity;
using Dapper;
using Newtonsoft.Json;
using System.Data;

namespace LearnHub.Infrastructure
{
    public class JsonTypeHandler : SqlMapper.ITypeHandler
    {
        public void SetValue(IDbDataParameter parameter, object value)
        {
            parameter.Value = JsonConvert.SerializeObject(value);
        }

        public object Parse(Type destinationType, object value)
        {
            return JsonConvert.DeserializeObject(value as string, destinationType);
        }
    }
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
            services.AddScoped<IIdentityService,IdentityService>();
            services.AddScoped<IDapperService, DapperService>();

            services.AddDbContext<ApplicationDbContext>(option =>
            {
                option.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));

            });

            services.AddDefaultIdentity<User>()
                .AddEntityFrameworkStores<ApplicationDbContext>().AddErrorDescriber<PersianIdentityErrorDescriber>();

            return services;
        }
    }
}

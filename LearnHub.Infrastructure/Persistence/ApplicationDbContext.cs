using System.Reflection;
using Duende.IdentityServer.EntityFramework.Options;
using LearnHub.Application.Common.Interfaces;
using Learnhub.Domain.Entities.Course;
using LearnHub.Infrastructure.Identity;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace LearnHub.Infrastructure.Persistence
{
    public class ApplicationDbContext: IdentityDbContext<User,IdentityRole<Guid>,Guid>, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<CourseCategory> CourseCategories => Set<CourseCategory>();
    }
}

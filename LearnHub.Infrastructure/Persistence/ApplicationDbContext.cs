using System.Reflection;
using Learnhub.Domain.Entities.Course;
using Microsoft.EntityFrameworkCore;

namespace LearnHub.Infrastructure.Persistence
{
    public class ApplicationDbContext:DbContext,IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<CourseCategory> CourseCategories => Set<CourseCategory>();
    }
}

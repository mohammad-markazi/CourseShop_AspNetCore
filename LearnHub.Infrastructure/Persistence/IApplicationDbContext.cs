using Learnhub.Domain.Entities.Course;
using Microsoft.EntityFrameworkCore;

namespace LearnHub.Infrastructure.Persistence
{
    public interface IApplicationDbContext
    {
        DbSet<CourseCategory> CourseCategories { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}

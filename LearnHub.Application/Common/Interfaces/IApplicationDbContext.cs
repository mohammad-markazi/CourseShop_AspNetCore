using Microsoft.EntityFrameworkCore;

namespace LearnHub.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Learnhub.Domain.Entities.Course.CourseCategory> CourseCategories { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}

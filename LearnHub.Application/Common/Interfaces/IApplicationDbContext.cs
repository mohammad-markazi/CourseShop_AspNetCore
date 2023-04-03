using Microsoft.EntityFrameworkCore;

namespace LearnHub.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Learnhub.Domain.Entities.Course.CourseCategory> CourseCategories { get; }
        DbSet<Learnhub.Domain.Entities.Course.Course> Courses { get; }
        DbSet<Learnhub.Domain.Entities.Course.CourseEpisode> CourseEpisodes { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}

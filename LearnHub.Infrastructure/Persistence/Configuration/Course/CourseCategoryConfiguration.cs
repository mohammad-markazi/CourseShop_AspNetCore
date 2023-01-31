using Learnhub.Domain.Entities.Course;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearnHub.Infrastructure.Persistence.Configuration.Course
{
    public class CourseCategoryConfiguration:IEntityTypeConfiguration<CourseCategory>
    {
        public void Configure(EntityTypeBuilder<CourseCategory> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(200);

            builder.OwnsOne(x => x.Seo, opt =>
            {
                opt.Property(x => x.Slug).IsRequired().HasMaxLength(250);
                opt.Property(x => x.MetaDescription).IsRequired().HasMaxLength(500);
                opt.Property(x => x.Keywords).IsRequired().HasMaxLength(250);
            });

        }
    }
}

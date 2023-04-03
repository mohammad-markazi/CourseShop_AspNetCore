using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearnHub.Infrastructure.Persistence.Configuration.Course
{
    public class CourseConfiguration:IEntityTypeConfiguration<Learnhub.Domain.Entities.Course.Course>
    {
        public void Configure(EntityTypeBuilder<Learnhub.Domain.Entities.Course.Course> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Title).IsRequired().HasMaxLength(500);

            builder.OwnsOne(x => x.Seo, opt =>
            {
                opt.Property(x => x.Slug).IsRequired().HasMaxLength(250);
                opt.Property(x => x.MetaDescription).IsRequired().HasMaxLength(500);
                opt.Property(x => x.Keywords).IsRequired().HasMaxLength(250);
            });
        }
    }
}

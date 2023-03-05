using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearnHub.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearnHub.Infrastructure.Persistence.Configuration.Identity
{
    public class UserConfiguration:IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.FirstName).HasMaxLength(255).IsRequired(false);
            builder.Property(x => x.LastName).HasMaxLength(255).IsRequired(false);
            builder.Property(x => x.Description).HasMaxLength(1000).IsRequired(false);
            builder.Property(x => x.Profile).HasMaxLength(400).IsRequired(false);



        }
    }
}

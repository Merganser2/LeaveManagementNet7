using LeaveManagement.Web.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagement.Web.Configurations.Entities
{
    public class RoleSeedConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<IdentityRole> builder)
        {
            // Seeding role data
            builder.HasData(
                new IdentityRole
                {
                    Id = "106365ea-d8b3-480f-bfb1-d9d672e268e5",
                    Name = Roles.Administrator,
                    NormalizedName = Roles.Administrator.ToUpper()
                },
                new IdentityRole
                {
                    Id = "80677512-f094-129c-caf3-e3f287a268d3",
                    Name = Roles.User,
                    NormalizedName = Roles.User.ToUpper()
                }
            ); ;
        }
    }
}
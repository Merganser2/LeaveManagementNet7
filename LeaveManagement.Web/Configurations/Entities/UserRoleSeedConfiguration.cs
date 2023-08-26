using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LeaveManagement.Web.Configurations.Entities
{
    public class UserRoleSeedConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(
                new IdentityUserRole<string>
                {
                    UserId = "526375a2-e6a1-4d0a-aec3-e9a673fa68b4",
                    RoleId = "106365ea-d8b3-480f-bfb1-d9d672e268e5"
                },
                new IdentityUserRole<string>
                {
                    UserId = "ab7375d3-e6b3-4f0a-ae93-e9a673fa32c1",
                    RoleId = "80677512-f094-129c-caf3-e3f287a268d3"
                }
           );
        }
    }
}
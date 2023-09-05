using LeaveManagement.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace LeaveManagement.Web.Configurations.Entities
{
    public class UserSeedConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Employee> builder)
        {
            // Seeding role data
            var passwordHasher = new PasswordHasher<Employee>();
            builder.HasData(
                new Employee
                {
                    Id = "526375a2-e6a1-4d0a-aec3-e9a673fa68b4",
                    Firstname = "Isabella",
                    Lastname = "Reyes",
                    Email = "lm-admin@test.net",
                    NormalizedEmail = "LM-ADMIN@TEST.NET",
                    UserName = "lm-admin@test.net",
                    NormalizedUserName = "LM-ADMIN@TEST.NET",
                    PasswordHash = passwordHasher.HashPassword(null, "P@ssword1"), // Can we do this? User doesn't exist yet. 
                    EmailConfirmed = true,
                    DateOfBirth = DateTime.ParseExact("01/30/1984", "MM/dd/yyyy", CultureInfo.InvariantCulture),
                    DateJoined = DateTime.Now
                },
                new Employee
                {
                    Id = "ab7375d3-e6b3-4f0a-ae93-e9a673fa32c1",
                    Firstname = "Tobias",
                    Lastname = "Whale",
                    Email = "tobias@test.net",
                    NormalizedEmail = "TOBIAS@TEST.NET",
                    UserName = "tobias@test.net",
                    NormalizedUserName = "TOBIAS@TEST.NET",
                    PasswordHash = passwordHasher.HashPassword(null, "P@ssword2"), // Still don't like having to put plain-text pwd in code.
                    EmailConfirmed = true,
                    DateOfBirth = DateTime.ParseExact("10/31/1953", "MM/dd/yyyy", CultureInfo.InvariantCulture),
                    DateJoined = DateTime.Now
                }
            );    
        }
    }
}
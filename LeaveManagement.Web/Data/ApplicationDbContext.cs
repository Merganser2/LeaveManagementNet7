using LeaveManagement.Web.Configurations.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using LeaveManagement.Web.Models;

namespace LeaveManagement.Web.Data
{
    // Would inherit from just DbContext if not using Identity based tables.
    // IdentityDbContext's User object type is IdentityUser.
    // Because Employee extends IdentityUser class, add Employee type to IdentityDbContext
    public class ApplicationDbContext : IdentityDbContext<Employee>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new RoleSeedConfiguration());
            builder.ApplyConfiguration(new UserSeedConfiguration());
            builder.ApplyConfiguration(new UserRoleSeedConfiguration());
        }

        // Note the pluralization of property names - Trevoir recommends, "jury still out" on whether this is a best practice
        //  Tables will be created named according to these properties
        public DbSet<LeaveType> LeaveTypes { get; set; }
        public DbSet<LeaveAllocation> LeaveAllocations { get; set; }
        public DbSet<LeaveRequest> LeaveRequests { get; set; }  
    }
}
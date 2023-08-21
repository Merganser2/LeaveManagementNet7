using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

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
    }
}
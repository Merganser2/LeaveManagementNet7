using Microsoft.AspNetCore.Identity;

namespace LeaveManagement.Data
{
    // IdentityUser is the default User class that was used to scaffold the AspNetUsersTable
    //   To extend that table, we inherit from it and add to it 
    public class Employee : IdentityUser
    {
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? TaxId { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime DateJoined { get; set; }
    }
}
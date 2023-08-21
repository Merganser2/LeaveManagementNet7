namespace LeaveManagement.Web.Data
{
    public partial class BaseEntity
    {
        public int Id { get; set; } // Entity Framework will automatically make this an auto-incrementing(?) PK
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}
namespace LeaveManagement.Data
{
    // Note that this is an abstract class, cannot be instantiated on its own
    public abstract class BaseEntity
    {
        // Entity Framework will automatically make a property of type int named 'Id' OR '<TableName>Id'
        // a PK that is auto-incrementing ("SqlServer:Identity" for SQL Server). TODO: Try with postgres to see if does SERIAL
        public int Id { get; set; }  
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}
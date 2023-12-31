﻿using System.ComponentModel.DataAnnotations.Schema;

namespace LeaveManagement.Data
{
    public class LeaveAllocation : BaseEntity
    {
        public int NumberOfDays { get; set; }

        // Below is just the annotation. EF Core will automatically see
        //  the pattern below it (Tablename, TablenameId) and create FK from it
        [ForeignKey("LeaveTypeId")]
        public LeaveType LeaveType { get; set; }
        public int LeaveTypeId { get; set; }

        [ForeignKey("EmployeeTypeId")]
        public Employee Employee { get; set; }
        public string EmployeeId { get; set; }

        // Time period (year or whatever) in which allocation occurs
        public int Period { get; set; }
    }
}
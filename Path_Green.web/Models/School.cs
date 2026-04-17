using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Path_Green.web.Models
{
    public class School
    {
        public int SchoolID { get; set; }
        public string? SchoolName { get; set; }
        public string? EmailDomain { get; set; }
        public bool IsActive { get; set; } = true;

        public ICollection<User>? Users { get; set; }
    }
}

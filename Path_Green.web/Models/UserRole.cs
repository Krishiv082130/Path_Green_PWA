using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Path_Green.web.Models
{
    public class UserRole
    {
        public int UserRoleID { get; set; }
        public int UserID { get; set; }
        public int RoleID { get; set; }
        public DateTime AssignedAt { get; set; } = DateTime.Now;

        public User? User { get; set; }
        public Role? Role { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Path_Green.web.Models
{
    public class Role
    {
        public int RoleID { get; set; }
        public string? RoleName { get; set; }
        public string? Description { get; set; }

        public ICollection<UserRole>? UserRoles { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Path_Green.web.Models
{
    public class User
    {
        public int UserID { get; set; }
        public int? SchoolID { get; set; }
        public string? AuthProviderID { get; set; }
        public string? Email { get; set; }
        public bool EmailVerified { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public bool IsActive { get; set; } = true;

        public School? School { get; set; }

        public ICollection<UserRole>? UserRoles { get; set; }
        public ICollection<Order>? Orders { get; set; }
        public ICollection<Donation>? Donations { get; set; }
        public ICollection<Message>? SentMessages { get; set; }
        public ICollection<SurveyResponse>? SurveyResponses { get; set; }
    }
}

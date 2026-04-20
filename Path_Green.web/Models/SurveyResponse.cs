using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;    

namespace Path_Green.web.Models
{
    public class SurveyResponse
    {
        public IdentityUser? User { get; set; }
        public int SurveyResponseID { get; set; }
        public int SurveyID { get; set; }
        public string? UserID { get; set; }
        public int OrderID { get; set; }
        public DateTime SubmittedAt { get; set; }

        public Survey? Survey { get; set; }
        public Order? Order { get; set; }

        public ICollection<SurveyAnswer>? Answers { get; set; }
    }
}

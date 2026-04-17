using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Path_Green.web.Models
{
    public class SurveyResponse
    {
        public int SurveyResponseID { get; set; }
        public int SurveyID { get; set; }
        public int UserID { get; set; }
        public int OrderID { get; set; }
        public DateTime SubmittedAt { get; set; }

        public Survey? Survey { get; set; }
        public User? User { get; set; }
        public Order? Order { get; set; }

        public ICollection<SurveyAnswer>? Answers { get; set; }
    }
}

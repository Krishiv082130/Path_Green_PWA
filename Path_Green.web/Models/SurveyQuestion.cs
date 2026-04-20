using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Path_Green.web.Models
{
    public class SurveyQuestion
    {
        [Key]
        public int QuestionID { get; set; }
        public int SurveyID { get; set; }
        public string? QuestionText { get; set; }
        public string? QuestionType { get; set; }
        public bool IsRequired { get; set; }

        public Survey? Survey { get; set; }
        public ICollection<SurveyAnswer>? Answers { get; set; }
    }
}

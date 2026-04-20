using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Path_Green.web.Models
{
    public class SurveyAnswer
    {
        [Key]
        public int AnswerID { get; set; }
        public int SurveyResponseID { get; set; }
        public int QuestionID { get; set; }
        public string? AnswerValue { get; set; }

        public SurveyResponse? SurveyResponse { get; set; }
        public SurveyQuestion? SurveyQuestion { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Path_Green.web.Models
{
    public class Survey
    {
        public int SurveyID { get; set; }
        public string? SurveyName { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }

        public ICollection<SurveyQuestion>? Questions { get; set; }
    }
}

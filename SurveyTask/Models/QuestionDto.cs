using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SurveyTask.Models
{
    public class QuestionDto
    {
        public int Id { get; set; }
        public int SurveyTitle { get; set; } 
        public int SurveyId { get; set; } 
        public string AnswerText { get; set; }

    }

}
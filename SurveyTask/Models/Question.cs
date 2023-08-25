using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SurveyTask.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int SurveyId { get; set; } // Foreign key to Survey
        public Survey Survey { get; set; }
        public string AnswerText { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }

    }

}
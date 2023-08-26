using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SurveyTask.Models
{
    public class Question
    {
        public Guid QuestionId { get; set; }
        public string Text { get; set; }
        public Guid SurveyId { get; set; } // Foreign key to Survey
        public Survey Survey { get; set; }
        public string AnswerText { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }

    }

}
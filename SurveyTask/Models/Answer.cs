using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SurveyTask.Models
{
    public class Answer
    {
        public Guid AnswerId { get; set; }
        public Guid SubmittedSurveId { get; set; }
        public Guid QuestionId { get; set; }
        public string AnswerText { get; set; }

        public virtual Question Question { get; set; }
        public virtual SubmittedSurvey SubmittedSurvey { get; set; }
    }
}
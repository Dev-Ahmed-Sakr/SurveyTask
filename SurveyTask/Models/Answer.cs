using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SurveyTask.Models
{
    public class Answer
    {
        public int Id { get; set; }
        public string Text { get; set; }

        public int QuestionId { get; set; } // Foreign key to Question
        public Question Question { get; set; }
    }

}
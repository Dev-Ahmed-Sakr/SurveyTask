using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SurveyTask.Models
{
    public class SurveyAnswerViewModel
    {
        public Guid SurveyId { get; set; }
        public List<AnswerViewModel> Answers { get; set; }
    }

}
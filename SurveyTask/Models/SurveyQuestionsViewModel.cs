using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SurveyTask.Models
{
    public class SurveyQuestionsViewModel
    {
        public Guid SurveyId { get; set; }
        public string Title { get; set; }
        public List<QuestionViewModel> Questions { get; set; }
    }

}
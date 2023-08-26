using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SurveyTask.Models
{
    public class SubmittedSurvey
    {
        public SubmittedSurvey() 
        { 
            CreatedDate = DateTime.Now;
            Answers = new List<Answer>();
        }
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        
        public Guid SurveyId { get; set; }
        public Survey Survey { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }

    }


}
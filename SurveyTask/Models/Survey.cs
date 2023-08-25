using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SurveyTask.Models
{
    public class Survey
    {
        public Survey() 
        { 
           CreatedDate = DateTime.Now;
            Questions = new List<Question>();
        }
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsSubmitted { get; set; }
        public ICollection<Question> Questions { get; set; }
    }


}
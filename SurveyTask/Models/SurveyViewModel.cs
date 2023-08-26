using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SurveyTask.Models
{
    public class SurveyViewModel
    {
        public SurveyViewModel()
        {
            CreatedDate = DateTime.Now;
        }
        public Guid SurveyId { get; set; }
        [Required]
        public string Title { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsSubmitted { get; set; }
        public ICollection<Question> Questions { get; set; }
    }
}
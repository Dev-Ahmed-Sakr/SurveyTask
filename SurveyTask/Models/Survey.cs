﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SurveyTask.Models
{
    public class Survey
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public ICollection<Question> Questions { get; set; }
    }


}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SurveyTask.Models
{
    public class AnswerViewModel
    {
        public Guid QuestionId { get; set; }
        public string AnswerText { get; set; }
    }

}
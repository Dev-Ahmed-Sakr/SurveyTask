using SurveyTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SurveyTask.Controllers
{
    public class UserSurveysController : Controller
    {
        // GET: UserSurveys
        private SurveyTaskContext _context = new SurveyTaskContext(); // Your database context


        public ActionResult Index()
        {
            var surveys = _context.Surveys.ToList();
            return View(surveys);
        }

        public ActionResult AnswerSurvey(int id)
        {
            var survey = _context.Surveys.Include("Questions").SingleOrDefault(s => s.Id == id);
            if (survey == null)
            {
                return HttpNotFound();
            }

            return View(survey);
        }

        [HttpPost]
        public ActionResult AnswerSurvey(Survey survey)
        {
            foreach (var question in survey.Questions)
            {
                var textAnswer = new Answer
                {
                    QuestionId = question.Id,
                    AnswerText = question.AnswerText
                };
                _context.Answers.Add(textAnswer);

            }

            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
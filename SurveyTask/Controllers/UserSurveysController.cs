using SurveyTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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

        public ActionResult AnswerSurvey(Guid SurveyId)
        {
            var survey = _context.Surveys.Include("Questions").SingleOrDefault(s => s.SurveyId == SurveyId);
            if (survey == null)
            {
                return HttpNotFound();
            }
            return View(survey);
        }

        [HttpPost]
        public ActionResult AnswerSurvey(Survey model)
        {
            foreach (var question in model.Questions)
            {
                var textAnswer = new Answer
                {
                    QuestionId = question.QuestionId,
                    AnswerText = question.AnswerText
                };
                _context.Answers.Add(textAnswer);

            }
            _context.SaveChanges();

            return RedirectToAction("Index", "UserSurveys");
        }
        [HttpPost]
        public ActionResult SubmitAnswers(List<AnswerViewModel> answers,Guid SurveyId)
        {
            try
            {
                var surveyQuestions = _context.Surveys.Where(s=>s.SurveyId == SurveyId);
                foreach (var answer in answers)
                {
                    var question = _context.Questions.FirstOrDefault(q => q.QuestionId == answer.QuestionId);

                    if (question != null)
                    {
                        // Update the answer text for the question
                        question.AnswerText = answer.AnswerText;
                    }
                }

                _context.SaveChanges();

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = "Error submitting answers." });
            }
        }

    }
}
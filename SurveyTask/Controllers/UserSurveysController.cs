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
        public ActionResult SubmitAnswers(SurveyAnswerViewModel model)
        {
            try
            {
                var answers = new List<Answer>();
                SubmittedSurvey submittedSurvey = new SubmittedSurvey()
                {
                    Id = Guid.NewGuid(),
                    SurveyId = model.SurveyId,
                };
                foreach (var answer in model.Answers)
                {
                    // Update the answer text for the questi
                    var textAnswer = new Answer
                    {
                        AnswerId = Guid.NewGuid(),
                        QuestionId = answer.QuestionId,
                        AnswerText = answer.AnswerText,
                    };
                    answers.Add(textAnswer);
                }
                submittedSurvey.Answers = answers;
                _context.SubmittedSurvey.Add(submittedSurvey);
                _context.SaveChanges();

                return Json(new { success = true, error = "Success." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = "Error submitting answers." });
            }
        }

    }
}
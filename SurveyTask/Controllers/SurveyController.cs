using System;
using System.Data.Entity;
using System.Drawing.Printing;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.UI;
using PagedList; 
using SurveyTask.Models;

public class SurveyController : Controller
{
    private readonly SurveyTaskContext _context = new SurveyTaskContext();

    //public ActionResult Index(int? page)
    //{
    //    int pageSize = 10; // Number of surveys per page
    //    int pageNumber = (page ?? 1);

    //    var surveys = _context.Surveys.OrderByDescending(s => s.Id).ToPagedList(pageNumber, pageSize);
    //    return View(surveys);
    //}
    public ActionResult ManageSurvey(int page = 1, int pageSize = 10)
    {
        var surveys = _context.Surveys.Include("Questions").Where(survey => !survey.IsSubmitted).OrderByDescending(s => s.SurveyId)
                             .Skip((page - 1) * pageSize)
                             .Take(pageSize)
                             .ToList();

        var totalCount = _context.Surveys.Count();
        var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

        ViewBag.CurrentPage = page;
        ViewBag.HasPreviousPage = page > 1;
        ViewBag.HasNextPage = page < totalPages;
        ViewBag.Surveys = surveys;

        return View(new Survey());
    }

    //[HttpPost]
    //public ActionResult ManageSurvey(Survey survey)
    //{
    //    if (ModelState.IsValid)
    //    {
    //        _context.Surveys.Add(survey);
    //        _context.SaveChanges();
    //        ViewBag.Success = true;
    //        return Json(new { success = true });
    //    }
    //    else
    //    {
    //        ViewBag.Success = false;
    //        return Json(new { success = false });
    //    }
    //}
    [HttpPost]
    public ActionResult ManageSurvey(string surveyModelJson)
    {
        if (!string.IsNullOrEmpty(surveyModelJson))
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                var surveyData = serializer.Deserialize<SurveyViewModel>(surveyModelJson);

                if (ModelState.IsValid)
                {
                    
                    var newSurvey = new Survey
                    {
                        SurveyId = Guid.NewGuid(),
                        Title = surveyData.Title,
                    };

                    foreach (var questionData in surveyData.Questions)
                    {
                        var newQuestion = new Question
                        {
                            QuestionId = Guid.NewGuid() ,
                            SurveyId = newSurvey.SurveyId,
                            Text = questionData.Text,
                        };

                        newSurvey.Questions.Add(newQuestion);
                    }

                    _context.Surveys.Add(newSurvey);
                    _context.SaveChanges();

                    return Json(new { success = true, surveyId = newSurvey.SurveyId });
                }
                else
                {
                    var errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage);

                    return Json(new { success = false, errors = errors });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = "Error processing JSON data." });
            }
        }

        return Json(new { success = false, error = "No JSON data received." });
    }

    public ActionResult ViewSurvey(Guid id)
    {
        var survey = _context.Surveys.Include("Questions").SingleOrDefault(s => s.SurveyId == id);
        if (survey == null)
        {
            return HttpNotFound();
        }

        return View(survey);
    }

    public ActionResult EditSurvey(Guid id)
    {
        var survey = SelectSurvey(id);
        if (survey == null)
        {
            return HttpNotFound();
        }

        return PartialView(survey);
    }
    [HttpPost]
    public ActionResult EditSurvey(SurveyViewModel model)
    {
        if (ModelState.IsValid)
        {
            var surveyToUpdate = _context.Surveys
                .Include(s => s.Questions)
                .FirstOrDefault(s => s.SurveyId == model.SurveyId);

            if (surveyToUpdate != null)
            {
                surveyToUpdate.Title = model.Title;

                foreach (var question in model.Questions)
                {
                    var existingQuestion = surveyToUpdate.Questions.FirstOrDefault(q => q.QuestionId == question.QuestionId);

                    if (existingQuestion != null)
                    {
                        existingQuestion.Text = question.Text;
                    }
                    else
                    {
                        // Add new question to survey
                        var newQuestion = new Question
                        {
                            Text = question.Text,
                        };

                        surveyToUpdate.Questions.Add(newQuestion);
                    }
                }

                _context.SaveChanges();

                return RedirectToAction("ManageSurvey");
            }
            else
            {
                ModelState.AddModelError("", "Survey not found.");
            }
        }

        return View(model);
    }
    public ActionResult UserSubmittedSurveys(int page = 1, int pageSize = 10)
    {
        var submittedSurveys = _context.Surveys.Include("Questions").Where(survey => survey.IsSubmitted).OrderByDescending(s => s.SurveyId)
                     .Skip((page - 1) * pageSize)
                     .Take(pageSize)
                     .ToList();

        var totalCount = _context.Surveys.Count();
        var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

        ViewBag.CurrentPage = page;
        ViewBag.HasPreviousPage = page > 1;
        ViewBag.HasNextPage = page < totalPages;
        ViewBag.Surveys = submittedSurveys;
        return View(submittedSurveys);
    }

    public ActionResult ViewSubmittedSurvey(Guid id)
    {
        var survey = _context.Surveys
            .Include(s => s.Questions.Select(q => q.Answers))
            .FirstOrDefault(s => s.SurveyId == id && s.IsSubmitted);

        if (survey == null)
        {
            return HttpNotFound();
        }

        return PartialView(survey);
    }
    [HttpPost]
    public ActionResult EditSurvey(Survey model)
    {
        if (ModelState.IsValid)
        {
            var surveyToUpdate = _context.Surveys
                .Include("Questions")
                .FirstOrDefault(s => s.SurveyId == model.SurveyId);

            if (surveyToUpdate != null)
            {
                surveyToUpdate.Title = model.Title;

                foreach (var question in model.Questions)
                {
                    var existingQuestion = surveyToUpdate.Questions.FirstOrDefault(q => q.QuestionId == question.QuestionId);

                    if (existingQuestion != null)
                    {
                        existingQuestion.Text = question.Text;
                    }
                }

                _context.SaveChanges();

                return RedirectToAction("Create");
            }
            else
            {
                ModelState.AddModelError("", "Survey not found.");
            }
        }

        return View(model);
    }
    public SurveyViewModel SelectSurvey(Guid id)
    {
        var survey = _context.Surveys.Include("Questions").SingleOrDefault(s => s.SurveyId == id);
        if (survey == null) return null;
        return new SurveyViewModel { SurveyId = survey.SurveyId, IsSubmitted = survey.IsSubmitted,Questions = survey.Questions,Title = survey.Title };
    }
}

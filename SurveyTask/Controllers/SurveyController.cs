using System;
using System.Linq;
using System.Web.Mvc;
using PagedList; // Make sure you've installed PagedList.Mvc package
using SurveyTask.Models;

public class SurveyController : Controller
{
    private SurveyTaskContext _context; // Your database context

    public SurveyController()
    {
        
    }
    public SurveyController(SurveyTaskContext context)
    {
        _context = context;
    }

    public ActionResult Index(int? page)
    {
        int pageSize = 10; // Number of surveys per page
        int pageNumber = (page ?? 1);

        var surveys = _context.Surveys.OrderByDescending(s => s.Id).ToPagedList(pageNumber, pageSize);
        return View(surveys);
    }
    public ActionResult Create(int page = 1, int pageSize = 10)
    {
        var surveys = _context.Surveys.OrderByDescending(s => s.Id)
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

    [HttpPost]
    public ActionResult Create(Survey survey)
    {
        if (ModelState.IsValid)
        {
            _context.Surveys.Add(survey);
            _context.SaveChanges();
            return RedirectToAction("Create");
        }

        // If the model state is not valid, return the view with validation errors
        return View(survey);
    }

}

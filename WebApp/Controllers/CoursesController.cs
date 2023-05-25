using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Services;
using WebApp.Services.Interfaces;

namespace WebApp.Controllers;

public class CoursesController : Controller
{
    private readonly ICourseService _courseService;
    private readonly List<Course?> _courses;

    public CoursesController(ICourseService courseService)
    {
        _courseService = courseService;
        _courses = courseService.GetAll().ToList();
    }

    public IActionResult Index()
    {
        return View(_courses);
    }

    [Route("[controller]/{courseId:int}")]
    public IActionResult Groups(int courseId)
    {
        var groups = _courseService.GetGroups(courseId).ToList();

        if (groups.Count == 0)
        {
            groups.Add(new Group()
            {
                Course = _courses.FirstOrDefault(x => x!.Id == courseId)
            });
        }
        
        return View(groups);
    }
}
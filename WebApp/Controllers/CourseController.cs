using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Controllers;

public class CourseController : Controller
{
    private readonly ICourseService _courseService;

    public CourseController(ICourseService courseService)
    {
        _courseService = courseService;
    }

    public IActionResult Index()
    {
        var courses = _courseService.GetAll().ToList();
        return View(courses);
    }

    [Route("[controller]/{courseId:int}")]
    public IActionResult Groups(int courseId)
    {
        var groups = _courseService.GetGroups(courseId).ToList();
        return View(groups);
    }
}
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

public class CoursesController : Controller
{
    private readonly IReadable<Course, Group> _service;
    private readonly List<Course?> _courses;

    public CoursesController(IReadable<Course, Group> service)
    {
        _service = service;
        _courses = service.GetAll().ToList()!;
    }

    public IActionResult Index()
    {
        return View(_courses);
    }

    [Route("[controller]/{courseId:int}")]
    public IActionResult Groups(int courseId)
    {
        var groups = _service.GetCollection(courseId).ToList();

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
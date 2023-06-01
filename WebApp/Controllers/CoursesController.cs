using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

public class CoursesController : Controller
{
    private readonly ICourseService<Course> _courseService;

    public CoursesController(ICourseService<Course> courseService)
    {
        _courseService = courseService;
    }

    public async Task<IActionResult> IndexAsync()
    {
        var courses = await _courseService.GetAllAsync() as List<Course>;
        return View("Index", courses);
    }

    public async Task<IActionResult> GroupsAsync(int courseId)
    {
        var courseGroups = await _courseService.GetCourseGroupsAsync(courseId) as List<Group>;
        var courses = await _courseService.GetAllAsync();
        
        ViewData["CourseName"] = courses.FirstOrDefault(x => x.Id == courseId)!.Name;
        
        return View("Groups", courseGroups);
    }
}
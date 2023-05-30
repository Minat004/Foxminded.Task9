using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Controllers;

public class GroupsController : Controller
{
    private readonly IService<Group, Student> _groupService;
    private readonly IReadable<Course> _courseService;
    private readonly List<Group> _groups;

    public GroupsController(IService<Group, Student> groupService, IReadable<Course> courseService)
    {
        _groupService = groupService;
        _courseService = courseService;
        _groups = groupService.GetAll().ToList();
    }

    public IActionResult Index()
    {
        return View(_groups);
    }

    public IActionResult Students(int groupId)
    {
        var students = _groupService.GetCollection(groupId).ToList();
        
        if (students.Count == 0)
        {
            students.Add(new Student()
            {
                Group = _groups.FirstOrDefault(x => x.Id == groupId)
            });
        }
        
        return View(students);
    }
    
    public IActionResult Edit(int groupId)
    {
        if (!string.IsNullOrEmpty(Request.Headers["Referer"].ToString()))
        {
            ViewData["Referer"] = Request.Headers["Referer"].ToString();
        }

        var group = _groups.FirstOrDefault(x => x.Id == groupId);
        
        return View(group);
    }
    
    [HttpPost]
    public IActionResult Edit(Group group)
    {
        if (ModelState.IsValid)
        {
            _groupService.Update(group);
            return RedirectToAction("Index");
        }

        return BadRequest();
        // return View(group);
    }
    
    public IActionResult Add()
    {
        if (!string.IsNullOrEmpty(Request.Headers["Referer"].ToString()))
        {
            ViewData["Referer"] = Request.Headers["Referer"].ToString();
        }
        
        var group = new Group
        {
            Courses = new SelectList(_courseService.GetAll(), "Id", "Name")
        };
        return View(group);
    }

    [HttpPost]
    public IActionResult Add(Group group)
    {
        if (ModelState.IsValid)
        {
            _groupService.Add(group);
            return RedirectToAction("Index");
        }
        
        return BadRequest();
        // return View(group);
    }

    [HttpPost]
    public IActionResult Delete(Group group)
    {
        if (group.Students.Count == 0)
        {
            _groupService.Delete(group);
        }

        return RedirectToAction("Index");
    }
}
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

    [Route("[controller]/{groupId:int}")]
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
    
    [Route("[controller]/{groupId:int}/[action]")]
    public IActionResult Edit(int groupId)
    {
        var group = _groups.FirstOrDefault(x => x.Id == groupId);
        
        return View(group);
    }
    
    [HttpPost]
    public IActionResult EditGroup(Group group)
    {
        _groupService.Update(group);

        return RedirectToAction("Index");
    }
    
    [Route("[controller]/[action]")]
    public IActionResult Add()
    {
        var group = new Group
        {
            Courses = new SelectList(_courseService.GetAll(), "Id", "Name")
        };
        return View(group);
    }

    [HttpPost]
    public IActionResult AddGroup(Group group)
    {
        _groupService.Add(group);
        
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult Delete(int groupId)
    {
        var group = _groupService.GetAll().FirstOrDefault(x => x.Id == groupId);

        if (group!.Students.Count == 0)
        {
            _groupService.Delete(group);
        }

        return RedirectToAction("Index");
    }
}
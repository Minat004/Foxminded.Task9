using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Controllers;

public class GroupsController : Controller
{
    private readonly IService<Group, Student> _groupService;
    private readonly IReadable<Course, Group> _courseService;
    private readonly ICancelable _cancelService;
    private readonly List<Group> _groups;

    public GroupsController(
        IService<Group, Student> groupService,
        IReadable<Course, Group> courseService,
        ICancelable cancelService)
    {
        _groupService = groupService;
        _courseService = courseService;
        _cancelService = cancelService;
        _groups = new List<Group>(groupService.GetAllAsync().Result);
    }

    public IActionResult Index()
    {
        return View(_groups);
    }

    public IActionResult Students(int groupId)
    {
        ViewData["GroupName"] = _groupService.GetAllAsync().Result.FirstOrDefault(x => x.Id == groupId)!.Name;
        
        var students = _groupService.GetCollectionAsync(groupId).Result.ToList();

        return View(students);
    }
    
    public IActionResult Edit(int groupId)
    {
        _cancelService.ViewDataReferer(ViewData, Request);

        var group = _groups.FirstOrDefault(x => x.Id == groupId);
        
        return View(group);
    }
    
    [HttpPost]
    public IActionResult Edit(Group group)
    {
        if (ModelState.IsValid)
        {
            _groupService.UpdateAsync(group);
            return RedirectToAction("Index");
        }

        return BadRequest();
    }
    
    public IActionResult Add()
    {
        _cancelService.ViewDataReferer(ViewData, Request);
        
        var group = new Group
        {
            Courses = new SelectList(_courseService.GetAllAsync().Result, "Id", "Name")
        };
        return View(group);
    }

    [HttpPost]
    public IActionResult Add(Group group)
    {
        if (ModelState.IsValid)
        {
            _groupService.AddAsync(group);
            return RedirectToAction("Index");
        }
        
        return BadRequest();
    }

    [HttpPost]
    public IActionResult Delete(Group group)
    {
        if (group.Students.Count == 0)
        {
            _groupService.DeleteAsync(group);
        }

        return RedirectToAction("Index");
    }
}
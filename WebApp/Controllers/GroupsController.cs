using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Controllers;

public class GroupsController : Controller
{
    private readonly IGroupService<Group> _groupService;
    private readonly ICourseService<Course> _courseService;
    private readonly ICancelable _cancelService;

    public GroupsController(
        IGroupService<Group> groupService,
        ICourseService<Course> courseService,
        ICancelable cancelService)
    {
        _groupService = groupService;
        _courseService = courseService;
        _cancelService = cancelService;
    }

    public async Task<IActionResult> IndexAsync()
    {
        var groups = await _groupService.GetAllAsync() as List<Group>;
        return View("Index", groups);
    }

    public async Task<IActionResult> StudentsAsync(int groupId)
    {
        var groupStudents = await _groupService.GetGroupStudentsAsync(groupId) as List<Student>;
        var groups = await _groupService.GetAllAsync();
        
        ViewData["GroupName"] = groups.FirstOrDefault(x => x.Id == groupId)!.Name;

        return View("Students", groupStudents);
    }
    
    public async Task<IActionResult> EditAsync(int groupId)
    {
        _cancelService.ViewDataReferer(ViewData, Request);

        var groups = await _groupService.GetAllAsync();
        var group = groups.FirstOrDefault(x => x.Id == groupId);
        
        if (group != null)
        {
            return View("Edit", group);
        }

        return NotFound();
    }
    
    [HttpPost]
    public async Task<IActionResult> EditAsync(Group group)
    {
        if (ModelState.IsValid)
        {
            await _groupService.UpdateAsync(group);
            return RedirectToAction("Index");
        }

        return BadRequest();
    }
    
    public async Task<IActionResult> AddAsync()
    {
        _cancelService.ViewDataReferer(ViewData, Request);
        
        var group = new Group
        {
            Courses = new SelectList(await _courseService.GetAllAsync(), "Id", "Name")
        };
        
        return View("Add", group);
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync(Group group)
    {
        if (ModelState.IsValid)
        {
            await _groupService.AddAsync(group);
            return RedirectToAction("Index");
        }
        
        return BadRequest();
    }

    [HttpPost]
    public async Task<IActionResult> DeleteAsync(Group group)
    {
        if (group.Students.Count == 0)
        {
            await _groupService.DeleteAsync(group);
        }

        return RedirectToAction("Index");
    }
}
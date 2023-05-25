using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using WebApp.Models;
using WebApp.Services;
using WebApp.Services.Interfaces;

namespace WebApp.Controllers;

public class GroupsController : Controller
{
    private readonly IGroupService _groupService;
    private readonly List<Group> _groups;

    public GroupsController(IGroupService groupService)
    {
        _groupService = groupService;
        _groups = groupService.GetAll().ToList();
    }

    public IActionResult Index()
    {
        return View(_groups);
    }

    [Route("[controller]/{groupId:int}")]
    public IActionResult Students(int groupId)
    {
        var students = _groupService.GetStudents(groupId).ToList();
        
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
        _groupService.UpdateName(group.Id, group.Name);

        return RedirectToAction("Index");
    }
    
    [Route("[controller]/[action]")]
    public IActionResult Add()
    {
        return View();
    }
}
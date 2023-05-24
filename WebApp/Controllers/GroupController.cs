using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Controllers;

public class GroupController : Controller
{
    private readonly IGroupService _groupService;

    public GroupController(IGroupService groupService)
    {
        _groupService = groupService;
    }

    public IActionResult Index()
    {
        var groups = _groupService.GetAll().ToList();
        return View(groups);
    }

    [Route("[controller]/{groupId:int}")]
    public IActionResult Students(int groupId)
    {
        var students = _groupService.GetStudents(groupId).ToList();
        return View(students);
    }
}
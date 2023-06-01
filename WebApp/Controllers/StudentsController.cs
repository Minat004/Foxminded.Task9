using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Controllers;

public class StudentsController : Controller
{
    private readonly IService<Student> _studentService;
    private readonly IGroupService<Group> _groupService;
    private readonly ICancelable _cancelService;

    public StudentsController(
        IService<Student> studentService,
        IGroupService<Group> groupService,
        ICancelable cancelService)
    {
        _studentService = studentService;
        _groupService = groupService;
        _cancelService = cancelService;
    }

    public async Task<IActionResult> IndexAsync()
    {
        var students = await _studentService.GetAllAsync() as List<Student>;
        return View("Index", students);
    }
    
    public async Task<IActionResult> EditAsync(int studentId)
    {
        _cancelService.ViewDataReferer(ViewData, Request);

        var students = await _studentService.GetAllAsync();
        var student = students.FirstOrDefault(x => x.Id == studentId);

        if (student != null)
        {
            return View("Edit", student);
        }

        return NotFound();
    }
    
    [HttpPost]
    public async Task<IActionResult> EditAsync(Student student)
    {
        if (ModelState.IsValid)
        {
            await _studentService.UpdateAsync(student);
            return RedirectToAction("Index");
        }

        return BadRequest();
    }
    
    public async Task<IActionResult> AddAsync()
    {
        _cancelService.ViewDataReferer(ViewData, Request);
        
        var student = new Student
        {
            Groups = new SelectList(await _groupService.GetAllAsync(), "Id", "Name")
        };
        
        return View("Add", student);
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync(Student student)
    {
        if (ModelState.IsValid)
        {
            await _studentService.AddAsync(student);
            return RedirectToAction("Index");
        }
        
        return BadRequest();
    }

    [HttpPost]
    public async Task<IActionResult> DeleteAsync(Student student)
    {
        await _studentService.DeleteAsync(student);
        return RedirectToAction("Index");
    }
}
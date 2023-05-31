using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Controllers;

public class StudentsController : Controller
{
    private readonly IService<Student> _studentService;
    private readonly IService<Group, Student> _groupService;
    private readonly ICancelable _cancelService;
    private readonly List<Student> _students;

    public StudentsController(
        IService<Student> studentService,
        IService<Group, Student> groupService,
        ICancelable cancelService)
    {
        _studentService = studentService;
        _groupService = groupService;
        _cancelService = cancelService;
        _students = new List<Student>(studentService.GetAllAsync().Result);
    }

    public IActionResult Index()
    {
        return View(_students);
    }
    
    public IActionResult Edit(int studentId)
    {
        _cancelService.ViewDataReferer(ViewData, Request);
        
        var student = _students.FirstOrDefault(x => x.Id == studentId);
        
        return View(student);
    }
    
    [HttpPost]
    public IActionResult Edit(Student student)
    {
        if (ModelState.IsValid)
        {
            _studentService.UpdateAsync(student);
            return RedirectToAction("Index");
        }

        return BadRequest();
    }
    
    public IActionResult Add()
    {
        _cancelService.ViewDataReferer(ViewData, Request);
        
        var student = new Student
        {
            Groups = new SelectList(_groupService.GetAllAsync().Result, "Id", "Name")
        };
        
        return View(student);
    }

    [HttpPost]
    public IActionResult Add(Student student)
    {
        if (ModelState.IsValid)
        {
            _studentService.AddAsync(student);
            return RedirectToAction("Index");
        }
        
        return BadRequest();
    }

    [HttpPost]
    public IActionResult Delete(Student student)
    {
        _studentService.DeleteAsync(student);

        return RedirectToAction("Index");
    }
}
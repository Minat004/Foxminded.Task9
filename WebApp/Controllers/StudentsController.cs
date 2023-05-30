using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Controllers;

public class StudentsController : Controller
{
    private readonly IService<Student> _studentService;
    private readonly IService<Group, Student> _groupService;
    private readonly List<Student> _students;

    public StudentsController(IService<Student> studentService, IService<Group, Student> groupService)
    {
        _studentService = studentService;
        _groupService = groupService;
        _students = studentService.GetAll().ToList();
    }

    public IActionResult Index()
    {
        return View(_students);
    }
    
    public IActionResult Edit(int studentId)
    {
        var student = _students.FirstOrDefault(x => x.Id == studentId);
        
        return View(student);
    }
    
    [HttpPost]
    public IActionResult Edit(Student student)
    {
        if (ModelState.IsValid)
        {
            _studentService.Update(student);
            return RedirectToAction("Index");
        }

        return BadRequest();
        // return View(student);
    }
    
    public IActionResult Add()
    {
        var student = new Student
        {
            Groups = new SelectList(_groupService.GetAll(), "Id", "Name")
        };
        
        return View(student);
    }

    [HttpPost]
    public IActionResult Add(Student student)
    {
        if (ModelState.IsValid)
        {
            _studentService.Add(student);
            return RedirectToAction("Index");
        }
        
        return BadRequest();
        // return View(student);
    }

    [HttpPost]
    public IActionResult Delete(Student student)
    {
        _studentService.Delete(student);

        return RedirectToAction("Index");
    }
}
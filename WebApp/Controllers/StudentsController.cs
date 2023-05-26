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
    
    [Route("[controller]/{studentId:int}/[action]")]
    public IActionResult Edit(int studentId)
    {
        var student = _students.FirstOrDefault(x => x.Id == studentId);
        
        return View(student);
    }
    
    [HttpPost]
    public IActionResult EditStudent(Student student)
    {
        _studentService.Update(student);

        return RedirectToAction("Index");
    }
    
    [Route("[controller]/[action]")]
    public IActionResult Add()
    {
        var student = new Student
        {
            Groups = new SelectList(_groupService.GetAll(), "Id", "Name")
        };
        return View(student);
    }

    [HttpPost]
    public IActionResult AddStudent(Student student)
    {
        _studentService.Add(student);
        
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult Delete(int studentId)
    {
        var student = _studentService.GetAll().FirstOrDefault(x => x.Id == studentId);
        
        _studentService.Delete(student!);

        return RedirectToAction("Index");
    }
}
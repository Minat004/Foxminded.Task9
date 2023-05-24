using WebApp.Models;
using WebApp.Repositories;

namespace WebApp.Services;

public class StudentService : IStudentService
{
    private readonly IStudentRepository _studentRepository;

    public StudentService(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public IEnumerable<Student> GetAll() => _studentRepository.GetAll();
}
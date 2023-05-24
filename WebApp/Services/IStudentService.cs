using WebApp.Models;

namespace WebApp.Services;

public interface IStudentService
{
    public IEnumerable<Student> GetAll();
}
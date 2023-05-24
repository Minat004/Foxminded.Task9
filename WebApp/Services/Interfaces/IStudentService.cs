using WebApp.Models;

namespace WebApp.Services.Interfaces;

public interface IStudentService
{
    public IEnumerable<Student> GetAll();
}
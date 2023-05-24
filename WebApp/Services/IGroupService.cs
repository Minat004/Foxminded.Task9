using WebApp.Models;

namespace WebApp.Services;

public interface IGroupService
{
    public IEnumerable<Group> GetAll();

    public IEnumerable<Student> GetStudents(int id);
}
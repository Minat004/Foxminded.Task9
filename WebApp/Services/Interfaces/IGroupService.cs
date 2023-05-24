using WebApp.Models;

namespace WebApp.Services.Interfaces;

public interface IGroupService
{
    public IEnumerable<Group> GetAll();

    public IEnumerable<Student> GetStudents(int id);

    public void Update(Group group);
}
using WebApp.Models;

namespace WebApp.Services;

public interface ICourseService
{
    public IEnumerable<Course?> GetAll();

    public IEnumerable<Group> GetGroups(int id);
}
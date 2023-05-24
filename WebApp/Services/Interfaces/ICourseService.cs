using WebApp.Models;

namespace WebApp.Services.Interfaces;

public interface ICourseService
{
    public IEnumerable<Course?> GetAll();

    public IEnumerable<Group> GetGroups(int id);
}
using WebApp.Models;
using WebApp.Repositories;

namespace WebApp.Services;

public class CourseService : ICourseService
{
    private readonly ICourseRepository _courseRepository;

    public CourseService(ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    public IEnumerable<Course?> GetAll() => _courseRepository.GetAll();
    
    public IEnumerable<Group> GetGroups(int id)
    {
        return _courseRepository.GetAll().FirstOrDefault(x => x!.Id == id)!.Groups;
    }
}
using WebApp.Models;

namespace WebApp.Repositories;

public interface ICourseRepository
{
    public void Create();
    public IEnumerable<Course?> GetAll();
    public Task<IEnumerable<Course>> GetAllAsync();
    public void Update(Course course);
    public void Update(int id);
    public void Delete(Course course);
    public void Delete(int id);
}
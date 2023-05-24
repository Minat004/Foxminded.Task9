using WebApp.Models;

namespace WebApp.Repositories;

public interface ICourseRepository
{
    public IEnumerable<Course?> GetAll();
    public Task<IEnumerable<Course>> GetAllAsync();
}
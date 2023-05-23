using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Repositories;

public class CourseRepository : ICourseRepository
{
    private readonly UniversityDbContext _context;

    public CourseRepository(UniversityDbContext context)
    {
        _context = context;
    }

    public void Create()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Course> GetAll()
    {
        return _context.Courses!.ToList();
    }

    public async Task<IEnumerable<Course>> GetAllAsync()
    {
        return await _context.Courses!.ToListAsync();
    }

    public void Update(Course course)
    {
        throw new NotImplementedException();
    }

    public void Update(int id)
    {
        throw new NotImplementedException();
    }

    public void Delete(Course course)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }
}
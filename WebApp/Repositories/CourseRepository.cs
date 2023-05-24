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
    
    public IEnumerable<Course?> GetAll()
    {
        var courses = _context.Courses!.ToList();
        var groups = _context.Groups!.ToList();
        
        foreach (var course in courses)
        {
            course.Groups.ToList().AddRange(groups.Where(x => x.CourseId == course.Id));
            yield return course;
        }
    }

    public async Task<IEnumerable<Course>> GetAllAsync()
    {
        return await _context.Courses!.ToListAsync();
    }
}
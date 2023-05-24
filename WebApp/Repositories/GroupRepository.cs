using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Repositories;

public class GroupRepository : IGroupRepository
{
    private readonly UniversityDbContext _context;

    public GroupRepository(UniversityDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Group> GetAll()
    {
        var groups = _context.Groups!.ToList();
        var courses = _context.Courses!.ToList();
        var students = _context.Students!.ToList();
        
        foreach (var group in groups)
        {
            group.Course = courses.FirstOrDefault(x => x.Id == group.CourseId);
            group.Students.ToList().AddRange(students.Where(x => x.GroupId == group.Id));
            yield return group;
        }
    }

    public async Task<IEnumerable<Group>> GetAllAsync()
    {
        return await _context.Groups!.ToListAsync();
    }
}
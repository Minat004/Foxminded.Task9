using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Repositories;

public class GroupRepository : IRepository<Group>
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

    public void Update(Group group)
    {
        _context.Groups!.Update(group);
        _context.SaveChanges();
    }

    public void Add(Group group)
    {
        _context.Groups!.Add(group);
        _context.SaveChanges();
    }

    public void Delete(Group group)
    {
        _context.Groups!.Remove(group);
        _context.SaveChanges();
    }
}
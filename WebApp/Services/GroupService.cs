namespace WebApp.Services;

public class GroupService : IService<Group, Student>
{
    private readonly UniversityDbContext _context;

    public GroupService(UniversityDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<Group>> GetAllAsync()
    {
        var groups = await _context.Groups!.ToListAsync();
        var courses = await _context.Courses!.ToListAsync();
        
        groups.ForEach(group =>
        {
            group.Course = courses.FirstOrDefault(course => course.Id == group.CourseId);
        });
        
        return groups;
    }

    public async Task<IEnumerable<Student>> GetCollectionAsync(int id)
    {
        return await _context.Students!.Where(x => x.GroupId == id).ToListAsync();
    }

    public async Task UpdateAsync(Group group)
    {
        var newGroup = _context.Groups!.FirstOrDefault(x => x.Id == group.Id);
        newGroup!.Name = group.Name;
        
        _context.Update(newGroup);
        await _context.SaveChangesAsync();
    }

    public async Task AddAsync(Group group)
    {
        _context.Add(group);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Group group)
    {
        _context.Remove(group);
        await _context.SaveChangesAsync();
    }
}
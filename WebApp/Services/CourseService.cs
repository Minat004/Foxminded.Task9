namespace WebApp.Services;

public class CourseService : IReadable<Course, Group>
{
    private readonly UniversityDbContext _context;

    public CourseService(UniversityDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<Course>> GetAllAsync()
    {
        return await _context.Courses!.ToListAsync();
    }

    public async Task<IEnumerable<Group>> GetCollectionAsync(int id)
    {
        return await _context.Groups!.Where(x => x.CourseId == id).ToListAsync();
    }
}
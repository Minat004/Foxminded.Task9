namespace WebApp.Services;

public class CourseService : ICourseService<Course>
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

    public async Task<IEnumerable<Group>> GetCourseGroupsAsync(int courseId)
    {
        return await _context.Groups!.Where(x => x.CourseId == courseId).ToListAsync();
    }
}
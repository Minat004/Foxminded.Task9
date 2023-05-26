namespace WebApp.Repositories;

public class StudentRepository : IRepository<Student>
{
    private readonly UniversityDbContext _context;

    public StudentRepository(UniversityDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Student> GetAll()
    {
        var students = _context.Students!.ToList();
        var groups = _context.Groups!.ToList();

        foreach (var student in students)
        {
            student.Group = groups.FirstOrDefault(x => x.Id == student.GroupId);
            yield return student;
        }
    }

    public async Task<IEnumerable<Student>> GetAllAsync()
    {
        return await _context.Students!.ToListAsync();
    }

    public void Update(Student group)
    {
        _context.Students!.Update(group);
        _context.SaveChanges();
    }

    public void Add(Student student)
    {
        _context.Students!.Add(student);
        _context.SaveChanges();
    }

    public void Delete(Student student)
    {
        _context.Students!.Remove(student);
        _context.SaveChanges();
    }
}
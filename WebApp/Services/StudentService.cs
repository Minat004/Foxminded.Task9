namespace WebApp.Services;

public class StudentService : IService<Student>
{
    private readonly UniversityDbContext _context;

    public StudentService(UniversityDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Student>> GetAllAsync()
    {
        var students = await _context.Students!.ToListAsync();
        var groups = await _context.Groups!.ToListAsync();

        students.ForEach(student =>
        {
            student.Group = groups.FirstOrDefault(group => group.Id == student.GroupId);
        });
        
        return students;
    }

    public async Task UpdateAsync(Student student)
    {
        var newStudent = _context.Students!.FirstOrDefault(x => x.Id == student.Id);
        newStudent!.FirstName = student.FirstName;
        newStudent.LastName = student.LastName;
        
        _context.Update(newStudent);
        await _context.SaveChangesAsync();
    }

    public async Task AddAsync(Student student)
    {
        _context.Add(student);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Student student)
    {
        _context.Remove(student);
        await _context.SaveChangesAsync();
    }
}
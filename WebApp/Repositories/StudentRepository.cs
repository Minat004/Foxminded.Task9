using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Repositories;

public class StudentRepository : IStudentRepository
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
}
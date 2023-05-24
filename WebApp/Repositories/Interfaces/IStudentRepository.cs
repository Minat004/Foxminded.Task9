using WebApp.Models;

namespace WebApp.Repositories.Interfaces;

public interface IStudentRepository
{
    public IEnumerable<Student> GetAll();
    
    public Task<IEnumerable<Student>> GetAllAsync();
}
namespace WebApp.Services;

public class StudentService : IService<Student>
{
    private readonly IRepository<Student> _repository;

    public StudentService(IRepository<Student> repository)
    {
        _repository = repository;
    }

    public IEnumerable<Student> GetAll() => _repository.GetAll();
    
    public Task<IEnumerable<Student>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public void Update(Student student)
    {
        var newStudent = _repository.GetAll().FirstOrDefault(x => x.Id == student.Id);
        newStudent!.FirstName = student.FirstName;
        newStudent.LastName = student.LastName;
        
        _repository.Update(newStudent);
    }

    public void Add(Student student)
    {
        _repository.Add(student);
    }

    public void Delete(Student student)
    {
        _repository.Delete(student);
    }
}
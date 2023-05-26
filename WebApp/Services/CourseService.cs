namespace WebApp.Services;

public class CourseService : IReadable<Course, Group>
{
    private readonly IReadable<Course> _repository;

    public CourseService(IReadable<Course> repository)
    {
        _repository = repository;
    }

    public IEnumerable<Course> GetAll() => _repository.GetAll();
    
    public Task<IEnumerable<Course>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Group> GetCollection(int id)
    {
        return _repository.GetAll().FirstOrDefault(x => x.Id == id)!.Groups;
    }
}
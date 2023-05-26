namespace WebApp.Services;

public class GroupService : IService<Group, Student>
{
    private readonly IRepository<Group> _repository;

    public GroupService(IRepository<Group> repository)
    {
        _repository = repository;
    }

    public IEnumerable<Group> GetAll() => _repository.GetAll();
    
    public Task<IEnumerable<Group>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Student> GetCollection(int id)
    {
        return _repository.GetAll().FirstOrDefault(x => x.Id == id)!.Students;
    }

    public void Update(Group group)
    {
        var newGroup = _repository.GetAll().FirstOrDefault(x => x.Id == group.Id);
        newGroup!.Name = group.Name;
        
        _repository.Update(newGroup);
    }

    public void Add(Group group)
    {
        _repository.Add(group);
    }

    public void Delete(Group group)
    {
        if (group.Students.Count != 0)
        {
            return;
        }
        
        _repository.Delete(group);
    }
}
using WebApp.Models;
using WebApp.Repositories;

namespace WebApp.Services;

public class GroupService : IGroupService
{
    private readonly IGroupRepository _groupRepository;

    public GroupService(IGroupRepository groupRepository)
    {
        _groupRepository = groupRepository;
    }

    public IEnumerable<Group> GetAll() => _groupRepository.GetAll();

    public IEnumerable<Student> GetStudents(int id)
    {
        return _groupRepository.GetAll().FirstOrDefault(x => x.Id == id)!.Students;
    }
}
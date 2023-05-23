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

    public IEnumerable<Group> GetAll()
    {
        return _groupRepository.GetAll();
    }
}
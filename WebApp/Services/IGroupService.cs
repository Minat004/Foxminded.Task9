using WebApp.Models;

namespace WebApp.Services;

public interface IGroupService
{
    public IEnumerable<Group> GetAll();
}
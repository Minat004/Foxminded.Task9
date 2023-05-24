using WebApp.Models;

namespace WebApp.Repositories;

public interface IGroupRepository
{
    public IEnumerable<Group> GetAll();
    public Task<IEnumerable<Group>> GetAllAsync();
}
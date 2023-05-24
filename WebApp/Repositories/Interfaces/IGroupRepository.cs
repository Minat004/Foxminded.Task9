using WebApp.Models;

namespace WebApp.Repositories.Interfaces;

public interface IGroupRepository
{
    public IEnumerable<Group> GetAll();
    
    public Task<IEnumerable<Group>> GetAllAsync();

    public void Update(Group group);
}
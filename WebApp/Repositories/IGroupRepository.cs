using WebApp.Models;

namespace WebApp.Repositories;

public interface IGroupRepository
{
    public void Create();
    public IEnumerable<Group> GetAll();
    public Task<IEnumerable<Group>> GetAllAsync();
    public void Update(Group course);
    public void Update(int id);
    public void Delete(Group course);
    public void Delete(int id);
}
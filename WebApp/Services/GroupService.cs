namespace WebApp.Services;

public class GroupService : IGroupService<Group>
{
    private readonly UniversityDbContext _context;

    public GroupService(UniversityDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<Group>> GetAllAsync()
    {
        var groups = await _context.Groups!.Include(x => x.Course).ToListAsync();

        return groups;
    }

    public async Task<IEnumerable<Student>> GetGroupStudentsAsync(int groupId)
    {
        return await _context.Students!.Where(x => x.GroupId == groupId).ToListAsync();
    }

    public async Task UpdateAsync(Group group)
    {
        var newGroup = _context.Groups!.FirstOrDefault(x => x.Id == group.Id);
        newGroup!.Name = group.Name;
        
        _context.Update(newGroup);
        await _context.SaveChangesAsync();
    }

    public async Task AddAsync(Group group)
    {
        _context.Add(group);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Group group)
    {
        _context.Remove(group);
        await _context.SaveChangesAsync();
    }
}
namespace WebApp.Interfaces;

public interface IReadable<T>
{
    public Task<IEnumerable<T>> GetAllAsync();
}

public interface ICourseReadable
{
    public Task<IEnumerable<Group>> GetCourseGroupsAsync(int courseId);
}

public interface IGroupReadable
{
    public Task<IEnumerable<Student>> GetGroupStudentsAsync(int groupId);
}
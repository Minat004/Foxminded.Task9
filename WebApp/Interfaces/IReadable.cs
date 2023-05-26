namespace WebApp.Interfaces;

public interface IReadable<T>
{
    public IEnumerable<T> GetAll();
    
    public Task<IEnumerable<T>> GetAllAsync();
}

public interface IReadable<U, out V> : IReadable<U>
{
    public IEnumerable<V> GetCollection(int id);
}
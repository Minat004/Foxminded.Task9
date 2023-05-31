namespace WebApp.Interfaces;

public interface IReadable<T>
{
    public Task<IEnumerable<T>> GetAllAsync();
}

public interface IReadable<U, V> : IReadable<U>
{
    public Task<IEnumerable<V>> GetCollectionAsync(int id);
}
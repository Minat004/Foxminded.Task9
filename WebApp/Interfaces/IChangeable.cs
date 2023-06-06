namespace WebApp.Interfaces;

public interface IChangeable<in T>
{
    public Task UpdateAsync(T group);
    
    public Task AddAsync(T item);
    
    public Task DeleteAsync(T item);
}
namespace WebApp.Interfaces;

public interface IChangeable<in T>
{
    public void Update(T group);
    
    public void Add(T item);
    
    public void Delete(T item);
}
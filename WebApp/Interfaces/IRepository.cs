namespace WebApp.Interfaces;

public interface IRepository<T> : IReadable<T>, IChangeable<T>
{
}
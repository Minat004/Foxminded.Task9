namespace WebApp.Interfaces;

public interface IService<U, V> : IReadable<U, V>, IChangeable<U> { }

public interface IService<T> : IReadable<T>, IChangeable<T> { }
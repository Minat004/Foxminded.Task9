namespace WebApp.Interfaces;

public interface IService<T> : IReadable<T>, IChangeable<T> { }

public interface ICourseService<T> : ICourseReadable, IReadable<T> { }

public interface IGroupService<T> : IGroupReadable, IReadable<T>, IChangeable<T> { }
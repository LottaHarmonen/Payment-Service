namespace Payment_Service.Application.Interfaces;

public interface IRepository<T> where T : class?
{
    Task Add(T? entity);
    Task<T?> GetById(int id);

}
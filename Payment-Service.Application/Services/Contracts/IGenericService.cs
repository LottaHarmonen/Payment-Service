namespace Payment_Service.Application.Services.Contracts;

public interface IGenericService<T> where T : class
{
    Task Add(T? entity);
    Task<T?> Get(int id);

}
namespace Payment_Service.Application.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IPaymentRepository? Payments { get; set; }
    IRepository<T> Repository<T>() where T : class;
    Task Complete();
}
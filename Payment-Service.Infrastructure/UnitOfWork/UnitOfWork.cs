using Payment_Service.Application.Interfaces;
using Payment_Service.Infrastructure.Data;
using Payment_Service.Infrastructure.Repositories;

namespace Payment_Service.Infrastructure.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly PaymentDbContext _dbContext;
    public IPaymentRepository? Payments { get; set; }

    public UnitOfWork(
        PaymentDbContext dbContext,
        IPaymentRepository paymentRepository
        )
    {
        _dbContext = dbContext;
        Payments = paymentRepository;
    }

    public IRepository<T> Repository<T>() where T : class
    {
        return new Repository<T>(_dbContext);
    }

    public async Task Complete()
    {
        try
        {
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {

            throw new ArgumentException("An error occurred while committing changes to the database.", ex);
        }
    }

    public void Dispose()
    {
        _dbContext.Dispose();
    }
}
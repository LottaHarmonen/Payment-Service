using Microsoft.EntityFrameworkCore;
using Payment_Service.Application.Interfaces;
using Payment_Service.Infrastructure.Data;

namespace Payment_Service.Infrastructure.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly DbSet<T?> _dbSet;
    private readonly PaymentDbContext _dbContext;

    public Repository(PaymentDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = dbContext.Set<T>();
    }

    public async Task Add(T? entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public async Task<T?> GetById(int id)
    {
        return await _dbSet.FindAsync(id);
    }
}
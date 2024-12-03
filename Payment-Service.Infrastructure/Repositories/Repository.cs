using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Payment_Service.Application.Interfaces;
using Payment_Service.Core.Entities;
using Payment_Service.Infrastructure.Data;

namespace Payment_Service.Infrastructure.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly DbSet<T?> _dbSet;
    private readonly MyDbContext _dbContext;

    public Repository(MyDbContext dbContext)
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
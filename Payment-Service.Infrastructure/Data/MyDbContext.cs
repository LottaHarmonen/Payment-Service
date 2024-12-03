using Microsoft.EntityFrameworkCore;
using Payment_Service.Core.Entities;

namespace Payment_Service.Infrastructure.Data;

public class MyDbContext(DbContextOptions<MyDbContext> options) : DbContext(options)
{
    public DbSet<Payment> Payments { get; set; }

}
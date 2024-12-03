using Microsoft.EntityFrameworkCore.Migrations;
using Payment_Service.Application.Interfaces;
using Payment_Service.Core.Entities;
using Payment_Service.Infrastructure.Data;

namespace Payment_Service.Infrastructure.Repositories;

public class PaymentRepository : Repository<Payment>, IPaymentRepository
{
    public PaymentRepository(MyDbContext dbContext) : base(dbContext)
    {
    }
}
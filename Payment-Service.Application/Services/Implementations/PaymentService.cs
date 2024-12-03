using Payment_Service.Application.Interfaces;
using Payment_Service.Application.Services.Contracts;
using Payment_Service.Core.Entities;

namespace Payment_Service.Application.Services.Implementations;

public class PaymentService : GenericService<Payment>, IPaymentService
{
    public PaymentService(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }
}
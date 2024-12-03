using Payment_Service.Application.Interfaces;
using Payment_Service.Application.Services.Contracts;

namespace Payment_Service.Application.Services.Implementations;

public class GenericService<T> : IGenericService<T> where T : class
{
    private readonly IRepository<T> _repository;
    private readonly IUnitOfWork _unitOfWork;

    public GenericService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _repository = _unitOfWork.Repository<T>();
    }

    public async Task Add(T? entity)
    {
        await _repository.Add(entity);
    }

    public async Task<T?> Get(int id)
    {
        return await _repository.GetById(id);
    }
}
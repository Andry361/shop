using Shop.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Domain.Core;
using Shop.Domain.Interfaces;
using Shop.Services.Logic.Exceptions;

namespace Shop.Services.Logic
{
  public abstract class BaseService<T> : IBaseService<T>
    where T : Shop.Domain.Core.Entity
  {
    private IRepository<T> _entityRepository;
    private IValidationService<T> _validationService;

    public BaseService(IRepository<T> entityRepository, IValidationService<T> validationService)
    {
      if (entityRepository == null)
        throw new ArgumentNullException("entityRepository");
      if (validationService == null)
        throw new ArgumentNullException("validationService");
      _entityRepository = entityRepository;
      _validationService = validationService;
    }
    public void Create(T entity)
    {
      BefoSave(entity);
      _entityRepository.Create(entity);
    }
    public void Delete(Guid id)
    {
      _entityRepository.Delete(id);
    }

    public IList<T> GetAll()
    {
      return _entityRepository.GetAll().ToList();
    }

    public T GetById(Guid id)
    {
      return _entityRepository.GetById(id);
    }

    public void Update(T entity)
    {
      _entityRepository.Update(entity);
    }
    private void BefoSave(T entity)
    {
      Validate(entity);
      OnBefoSave(entity);
    }
    private void Validate(T entity)
    {
      var message = _validationService.Validate(entity);
      if (!string.IsNullOrWhiteSpace(message))
      {
        throw new ValidationException(message);
      }
    }
    protected virtual void OnBefoSave(T entity) { }
  }
}

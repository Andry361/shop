using FluentNHibernate.Data;
using NHibernate;
using NHibernate.Linq;
using Shop.Domain.Core;
using Shop.Domain.Interfaces;
using Shop.Infrastructure.Data.Helpers;
using Shop.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Infrastructure.Data.Repositories
{
  public class Repository<T> : IRepository<T> where T : Shop.Domain.Core.Entity
  {
    private UnitOfWork _unitOfWork;
    private readonly ITimeService _timeService;
    private readonly IAuthenticationService _authenticationService;
    IValidationService<T> _validationService;
    public Repository(IUnitOfWork unitOfWork, ITimeService timeService, IAuthenticationService authenticationService, IValidationService<T> validationService)
    {
      _unitOfWork = (UnitOfWork)unitOfWork;
      _timeService = timeService;
      _validationService = validationService;
    }

    protected ISession Session { get { return _unitOfWork.Session; } }

    public IQueryable<T> GetAll()
    {
      return Session.Query<T>();
    }

    public T GetById(Guid id)
    {
      return Session.Get<T>(id);
    }

    public void Create(T entity)
    {
      BefoSave(entity);
      Session.Save(entity);
    }

    public void Update(T entity)
    {
      Session.Update(entity);
    }

    public void Delete(Guid id)
    {
      Session.Delete(Session.Load<T>(id));
    }

    private void BefoSave(T entity)
    {
      if (IsAccessSave(entity))
      {
        FillBasicData(entity);
        OnBefoSave(entity);
      }
    }

    private void FillBasicData(T entity)
    {
      if (entity.IsNew())
      {
        entity.CreationDateTime = _timeService.Now;
        if (_authenticationService.CurrentUserId.HasValue && _authenticationService.CurrentUser != null)
        {
          entity.CreatorId = _authenticationService.CurrentUserId.Value;
          entity.CreatorName = _authenticationService.CurrentUser.DisplayName;
        }
      }
    }

    private bool IsAccessSave(T entity)
    {
      return true;
    }

    protected virtual void OnBefoSave(T entit) { }
  }
}

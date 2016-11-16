using FluentNHibernate.Data;
using NHibernate;
using NHibernate.Linq;
using Shop.Domain.Interfaces;
using Shop.Infrastructure.Data.Helpers;
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
    public Repository(IUnitOfWork unitOfWork)
    {
      _unitOfWork = (UnitOfWork)unitOfWork;
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
  }
}

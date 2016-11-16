using Shop.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Interfaces
{
  public interface IRepository<T> where T : Entity
  {
    IQueryable<T> GetAll();
    T GetById(Guid id);
    void Create(T entity);
    void Update(T entity);
    void Delete(Guid id);
  }
}

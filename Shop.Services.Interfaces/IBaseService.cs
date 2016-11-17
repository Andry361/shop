using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Services.Interfaces
{
  public interface IBaseService<T> where T : Shop.Domain.Core.Entity
  {
    IList<T> GetAll();
    T GetById(Guid id);
    void Create(T entity);
    void Update(T entity);
    void Delete(Guid id);
  }
}

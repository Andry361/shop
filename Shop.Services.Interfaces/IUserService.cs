using Shop.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Services.Interfaces
{
  public interface IUserService
  {
    IList<User> GetAll();
    User GetById(Guid id);
    void Create(User product);
    void Update(User product);
    void Delete(Guid id);
  }
}

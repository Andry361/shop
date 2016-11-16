using Shop.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Interfaces
{
  interface IUserRepository
  {
    IEnumerable<User> GetUsers();
    User GetUser(Guid id);
    void Create(User user);
    void Update(User user);
    void Delete(Guid id);
    void Save();
  }
}

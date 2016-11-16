using Shop.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Services.Interfaces
{
  interface IAccessControlManager
  {
    void Authorize(User user);
  }
}

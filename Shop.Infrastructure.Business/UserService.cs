using Shop.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Domain.Core;
using Shop.Domain.Interfaces;
using Shop.Services.Logic;

namespace Shop.Infrastructure.Business
{
  public class UserService : BaseService<User>, IUserService
  {
    public UserService(IRepository<User> entityRepository, IValidationService<User> validationService) : base(entityRepository, validationService)
    {
    }

  }
}

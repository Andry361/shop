using Shop.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Shop.Services.Interfaces.Enums;

namespace Shop.Services.Interfaces
{
  public interface IAuthenticationService
  {
    LoginResult Login(string loginName, string password);
    void LogOut();
    Guid? CurrentUserId { get;}
    User CurrentUser { get; }
    void RequiresAuthentication();
    bool IsAuthenticated { get; }
  }
}

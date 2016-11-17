using Shop.Domain.Core;
using Shop.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Shop.UI.Controllers
{
  public class UserController : ApiController
  {
    private IUserService _userService;
    public UserController(IUserService userService)
    {
      if (userService == null)
        throw new ArgumentNullException("userService");
      _userService = userService;
    }

    public IEnumerable<User> GetUsers()
    {
      return _userService.GetAll();
    }

    public void CreateUser(User user)
    {
      if (user == null)
        throw new ArgumentNullException("user == null. In UserController in method CreateUser.");
      _userService.Create(user);
    }
  }
}

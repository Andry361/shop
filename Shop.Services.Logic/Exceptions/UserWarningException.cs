using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Services.Logic.Exceptions
{
  public class UserWarningException : UserVisibleException
  {
    public UserWarningException(string userMessage)
      : base(userMessage, title: Properties.Resources.Warning)
    {
    }
  }
}

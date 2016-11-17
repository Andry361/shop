using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Services.Logic.Exceptions
{
  public class UserErrorException : UserVisibleException
  {
    public UserErrorException(string userMessage = null, Exception innerException = null)
      : base(string.IsNullOrWhiteSpace(userMessage) ? Properties.Resources.SystemError : userMessage, title: Properties.Resources.Error, innerException: innerException)
    {
    }
  }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Services.Logic.Exceptions
{
  public class UserVisibleException : ApplicationException//, IDoNotLogException
  {
    public string Title { get; set; }

    public UserVisibleException(string userMessage, Exception innerException = null, string title = null)
      : base(userMessage, innerException)
    {
      if (string.IsNullOrWhiteSpace(title))
      {
        title = Properties.Resources.Error;
      }
      Title = title;
    }
  }
}

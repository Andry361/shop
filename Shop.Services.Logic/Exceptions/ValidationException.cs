using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Services.Logic.Exceptions
{
  public class ValidationException : UserWarningException
  {
    public ValidationException(string message, params object[] args) : base(string.Format(message, args)) { }
    public ValidationException(string message) : base(message) { }
  }
}

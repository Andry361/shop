using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Services.Interfaces
{
  public interface IValidationService<T> where T : class
  {
    string Validate(T objectToValidate);
    void ValidateAndThrow(T objectToValidate);
  }
}

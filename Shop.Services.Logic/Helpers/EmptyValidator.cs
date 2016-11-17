using Shop.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Services.Logic
{
  public sealed class EmptyValidator<T> : IValidator<T> where T : class
  {
    public IEnumerable<IValidationRule<T>> ValidationRules
    {
      get { return new List<IValidationRule<T>>(); }
    }
  }
}

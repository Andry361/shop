using Shop.Domain.Interfaces;
using Shop.Services.Interfaces;
using Shop.Services.Logic.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Services.Logic.Helpers
{
  public class ValidationService<T> : IValidationService<T> where T : class
  {
    IEnumerable<IValidator<T>> _Validators = null;
    public ValidationService(IEnumerable<IValidator<T>> validators)
    {
      if (validators == null) throw new ArgumentNullException("validators");
      _Validators = validators;
    }

    public void ValidateAndThrow(T objectToValidate)
    {
      var message = Validate(objectToValidate);
      if (!string.IsNullOrWhiteSpace(message)) throw new ValidationException(message);
    }

    public string Validate(T objectToValidate)
    {
      if (objectToValidate == null) throw new ArgumentNullException("objectToValidate");

      var messages = new List<string>();

      foreach (var validator in _Validators)
      {
        if (validator.ValidationRules == null)
        {
          //L.Warn("validator.ValidationRules == null in {0}", validator.GetType());
          continue;
        }
        var validationRules = validator.ValidationRules.Where(x => x != null).ToList();

        messages.AddRange(validationRules.Select(x => x.Validate(objectToValidate))
                                         .Where(x => !string.IsNullOrWhiteSpace(x)));
      }

      var result = "";
      if (messages.Count > 0)
      {
        result = messages.Aggregate((x, c) => string.Format("{0}{1}{2}", x, Environment.NewLine, c));
      }

      return result;
    }
  }
}

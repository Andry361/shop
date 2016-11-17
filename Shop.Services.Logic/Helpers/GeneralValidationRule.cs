using Shop.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Services.Logic.Helpers
{
  public class GeneralValidationRule<T> : IValidationRule<T> where T : class
  {
    Expression<Func<T, bool>> _Condition;
    Expression<Func<T, string>> _Message;

    public GeneralValidationRule<T> Condition(Expression<Func<T, bool>> condition)
    {
      _Condition = condition;
      return this;
    }

    public GeneralValidationRule<T> Message(Expression<Func<T, string>> message)
    {
      _Message = message;
      return this;
    }

    public string Validate(T objectToValidate)
    {
      if (objectToValidate == null || !(objectToValidate is T)) throw new ArgumentNullException("objectToValidate");

      if (_Condition == null)
        return string.Empty;
      else
        return _Condition.Compile()(objectToValidate as T) ? _Message.Compile()(objectToValidate as T) : "";
    }
  }
}

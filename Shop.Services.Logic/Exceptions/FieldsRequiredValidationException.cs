using Shop.Services.Logic.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Services.Logic.Exceptions
{
  public class FieldsRequiredValidationException : ValidationException
  {
    public FieldsRequiredValidationException(params string[] fieldsProgramNames): base(GetMessage(fieldsProgramNames)) { }

    static string GetMessage(params string[] fieldsProgramNames)
    {
      if (fieldsProgramNames == null || fieldsProgramNames.Count() == 0)
      {
        return Properties.Resources.YouDidNotFillAllTheRequiredFields;
      }
      else if (fieldsProgramNames.Count() == 1)
      {
        return string.Format(Properties.Resources.FieldIsRequired, fieldsProgramNames.First().Localize());
      }
      else
      {
        return string.Format(Properties.Resources.FieldsAreRequired, fieldsProgramNames.Select(x => string.Format("'{0}'", x.Localize())).Aggregate((c, v) => string.Format("{0}, {1}", c, v)));
      }
    }
  }
}

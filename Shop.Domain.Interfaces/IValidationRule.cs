using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Interfaces
{
  /// <summary>
  /// Описывает правило валидации объекта доменной модели
  /// </summary>
  public interface IValidationRule<T> where T : class
  {
    string Validate(T objectToValidate);
  }
}

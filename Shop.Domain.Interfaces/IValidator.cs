using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Interfaces
{
  /// <summary>
  /// Этот интерфейс должны реализовывать валидаторы объектов доменной модели
  /// </summary>
  public interface IValidator<T> where T : class
  {
    IEnumerable<IValidationRule<T>> ValidationRules { get; }
  }
}

using Shop.Domain.Interfaces;
using Shop.Services.Logic.Exceptions;
using Shop.Services.Logic.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Services.Logic
{
  // TODO: Этот класс надо будет дополнять различными типовыми вариантами валидации значения свойства
  public class PropertyValidationRule<T> : IValidationRule<T> where T : class
  {
    Expression<Func<T, object>> _Property = null;
    bool _IsRequired = false;

    public PropertyValidationRule<T> Property(Expression<Func<T, object>> property)
    {
      _Property = property;
      return this;
    }

    public string Validate(T objectToValidate)
    {
      if (objectToValidate == null) throw new ArgumentNullException("objectToValidate");
      if (_Property == null) return "";

      var result = "";
      var propertyInfo = _Property.GetPropertyInfo();
      var value = propertyInfo.GetValue(objectToValidate);

      if (_IsRequired)
      {
        if (value == null
            || (propertyInfo.PropertyType == typeof(string) && string.IsNullOrWhiteSpace((value as string)))
            || (propertyInfo.PropertyType.IsEnum && (int)value == 0)
            || (propertyInfo.PropertyType == typeof(int) && (int)value == 0)
            || (propertyInfo.PropertyType == typeof(long) && (long)value == 0)
            || (propertyInfo.PropertyType == typeof(decimal) && (decimal)value == 0)
            || (propertyInfo.PropertyType == typeof(double) && (double)value == 0)
            || (propertyInfo.PropertyType == typeof(float) && (float)value == 0)
            || (propertyInfo.PropertyType == typeof(short) && (short)value == 0)
            || (propertyInfo.PropertyType == typeof(ushort) && (ushort)value == 0)
            || (propertyInfo.PropertyType == typeof(ulong) && (ulong)value == 0)
            || (propertyInfo.PropertyType == typeof(uint) && (uint)value == 0)
            || (propertyInfo.PropertyType == typeof(byte) && (byte)value == 0)
            || (propertyInfo.PropertyType == typeof(sbyte) && (sbyte)value == 0))
        {
          result = string.Format(Properties.Resources.NeedToFillValueOfProperty, propertyInfo.GetPropertyDisplayName());
        }
      }

      return result;
    }

    public PropertyValidationRule<T> Required(bool isRequired = true)
    {
      _IsRequired = isRequired;
      return this;
    }
  }

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

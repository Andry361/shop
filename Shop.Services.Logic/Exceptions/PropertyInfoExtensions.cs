using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Services.Logic.Exceptions
{
  public static class PropertyInfoExtensions
  {
    /// <summary>
    /// Позволяет получить аттрибут типа AttributeT с учётом иерархии наследования.
    /// Возвращает первый аттрибут типа AttributeT, который встречается у этого свойства сверху вниз по иерархии наследования (т.е. просмотр свойств идёт от потомков к предкам)
    /// </summary>
    public static AttributeT GetCustomPropertyAttributeFromInheritance<AttributeT>(this PropertyInfo propertyInfo) where AttributeT : Attribute
    {
      //L.Debug("");
      //L.Debug("Enter method:");
      try
      {
        if (propertyInfo == null) throw new ArgumentNullException("propertyInfo");

        var attrib = propertyInfo.GetCustomAttributes(typeof(AttributeT), true).FirstOrDefault() as AttributeT;
        while (attrib == null && propertyInfo != null && propertyInfo.DeclaringType.BaseType != null)
        {
          propertyInfo = propertyInfo.DeclaringType.BaseType.GetProperty(propertyInfo.Name);
          if (propertyInfo != null)
          {
            attrib = propertyInfo.GetCustomAttributes(typeof(AttributeT), true).FirstOrDefault() as AttributeT;
          }
        }

        return attrib;
      }
      catch (Exception ex)
      {
        //L.Exception(ex, "General exception");
        throw;
      }
      finally
      {
        //L.Debug("Leave method");
        //L.Debug("");
      }
    }

    public static string GetPropertyDisplayName(this PropertyInfo propertyInfo)
    {
      // TODO:sanya Здесь можно сделать поиск строки не только по Name, но и с вариантами PropertyTypeName.Property.Name и т.д.
      return Strings.GetString(propertyInfo.Name);
    }
  }
}

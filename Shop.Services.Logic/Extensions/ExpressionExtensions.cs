using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Services.Logic.Extensions
{
  public static class ExpressionExtensions
  {
    // TODO:sanya GetPropertyName - Нужно переделать
    public static string GetPropertyName<ObjectT>(this Expression<Func<ObjectT, object>> property)
    {
      //L.Debug("");
      //L.Debug("Enter method: property={0}", property);
      try
      {
        if (property == null)
        {
          //L.Error("property is null");
          throw new ArgumentNullException("property");
        }

        try
        {
          var arr = property.Body.ToString().Replace("Convert", "").Replace("(", "").Replace(")", "").Split('.');
          if (arr.Length != 2)
          {
            throw new ArgumentException("Incorrect expression. You must pass a property of this business object");
          }
          return arr[1];
        }
        catch (Exception ex)
        {
          //L.Exception(ex, "General exception");
          throw;
        }
      }
      finally
      {
        //L.Debug("Leave method");
        //L.Debug("");
      }
    }

    public static PropertyInfo GetPropertyInfo<ObjectT>(this Expression<Func<ObjectT, object>> property)
    {
      //L.Debug("");
      //L.Debug("Enter method: property={0}", property);
      try
      {
        if (property == null)
        {
          //L.Error("property is null");
          throw new ArgumentNullException("property");
        }

        var propName = property.GetPropertyName();
        var pi = typeof(ObjectT).GetProperty(propName);
        return pi;
      }
      finally
      {
        //L.Debug("Leave method");
        //L.Debug("");
      }
    }

    public static string GetPropertyFullName(this Expression propertyExpression)
    {
      try
      {
        if (propertyExpression is MemberExpression)
          return GetPropertyName(propertyExpression as MemberExpression);
        else if (propertyExpression is UnaryExpression)
          return GetPropertyName((propertyExpression as UnaryExpression).Operand as MemberExpression);
        else if (propertyExpression is LambdaExpression)
        {
          return GetPropertyFullName((propertyExpression as LambdaExpression).Body);
        }
        else
          throw new ApplicationException(string.Format("Expression: {0} is not MemberExpression", propertyExpression));
      }
      catch (Exception ex)
      {
        //L.Exception(ex);
        throw;
      }
    }
    static string GetPropertyName(MemberExpression me)
    {
      string propertyName = me.Member.Name;

      if (me.Expression.NodeType == ExpressionType.Parameter
        || me.Expression.NodeType == ExpressionType.TypeAs
        || me.Expression.NodeType == ExpressionType.Constant)
      {
        // If the current MemberExpression is at the root object, set that as the target.            
      }
      else
      {
        propertyName = GetPropertyName(me.Expression as MemberExpression) + "." + propertyName;
      }

      // Return the value from current MemberExpression against the current target
      return propertyName;
    }
  }
}

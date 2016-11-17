using Shop.Services.Logic.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Resources;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Services.Logic
{
  /// <summary>
  /// Класс предназначен для сбора всех строковых ресурсов с текущего проекта.
  /// Позволяет получать доступ к любой строке по имени не зависимо, в какой сборке или файле эта строка находится.
  /// </summary>
  public class Strings
  {
    static object m_InitLocker = new object();

    public static List<ResourceManager> ResourceManagers
    {
      get;
      private set;
    }

    public static string GetString<ObjectT>(Expression<Func<ObjectT, object>> propertyExpression)
    {
      return GetString<ObjectT, object>(propertyExpression);
    }

    public static string GetString<ObjectT>(Expression<Func<ObjectT, object>> propertyExpression, out bool found)
    {
      return GetString<ObjectT, object>(propertyExpression, out found);
    }

    public static string GetString<ObjectT>(Expression<Func<ObjectT, object>> propertyExpression, IEnumerable<ResourceManager> resourceManagers, out bool found)
    {
      return GetString<ObjectT, object>(propertyExpression, resourceManagers, out found);
    }
    public static string GetString<ObjectT, PropertyT>(Expression<Func<ObjectT, PropertyT>> propertyExpression, out bool found)
    {
      string propertyName = propertyExpression.GetPropertyFullName();
      return GetString(propertyName, out found);
    }

    public static string GetString<ObjectT, PropertyT>(Expression<Func<ObjectT, PropertyT>> propertyExpression)
    {
      string propertyName = propertyExpression.GetPropertyFullName();
      bool found = false;
      return GetString(propertyName, out found);
    }

    public static string GetString<ObjectT, PropertyT>(Expression<Func<ObjectT, PropertyT>> propertyExpression, IEnumerable<ResourceManager> resourceManagers, out bool found)
    {
      string propertyName = propertyExpression.GetPropertyFullName();
      return GetString(propertyName, resourceManagers, out found);
    }

    public static string GetString(string name)
    {
      bool found = false;
      return GetString(name, out found);
    }

    public static string GetString(string name, out bool found)
    {
      return GetString(name, ResourceManagers, out found);
    }

    public static string GetString(string name, IEnumerable<ResourceManager> resourceManagers, out bool found)
    {
      string res = null;
      foreach (var rm in resourceManagers)
      {
        res = rm.GetString(name);
        if (!string.IsNullOrWhiteSpace(res)) break;
      }

      if (string.IsNullOrWhiteSpace(res))
      {
        res = string.Format("[Resource string not found: {0}]", name);
        found = false;
      }
      else
      {
        found = true;
      }

      return res;
    }

    public static Dictionary<string, string> GetAllStrings(CultureInfo culture = null)
    {
      if (culture == null)
      {
        culture = CultureInfo.CurrentUICulture;
      }
      var result = new Dictionary<string, string>();
      foreach (var rm in ResourceManagers)
      {
        var resourceSet = rm.GetResourceSet(culture, true, true);
        foreach (DictionaryEntry entry in resourceSet)
        {
          result[entry.Key as string] = entry.Value as string;
        }
      }

      return result;
    }

    static Strings()
    {
      lock (m_InitLocker)
      {
        ResourceManagers = new List<ResourceManager>();

        var resourcesTypes = new List<Type>();
        var loadedAssemblies = AssembliesMenager.GetAllAssemblies();
        foreach (var assm in loadedAssemblies)
        {
          resourcesTypes.AddRange(assm.GetTypes().Where(t => //String.Equals(t.Namespace, typeof(Utils).Namespace + ".Properties", StringComparison.Ordinal) &&
                t.GetProperty("ResourceManager", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly) != null
                && t.GetProperty("ResourceManager", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly).PropertyType == typeof(ResourceManager)).ToList());
        }

        foreach (var t in resourcesTypes)
        {
          ResourceManagers.Add(new ResourceManager(t));
        }
      }
    }

    static Random _RandomNotThreadSafe = null;
    static object _RandomLocker = new object();
    static DateTime? _RandomCreatedDateTime = null;

    static double ThreadSafeGetNextDouble()
    {
      lock (_RandomLocker)
      {
        if (_RandomNotThreadSafe == null || _RandomCreatedDateTime == null
              || (DateTime.UtcNow - _RandomCreatedDateTime.Value).TotalMinutes > 5)
        {
          _RandomNotThreadSafe = new Random((int)((DateTime.Now.Ticks << 32) >> 32));
          _RandomCreatedDateTime = DateTime.UtcNow;
        }

        var nextDouble = _RandomNotThreadSafe.NextDouble();
        if (nextDouble == null)
        {
          //L.Warn("ThreadSafeGetNextDouble returned a zero");
        }

        return nextDouble;
      }
    }

    public static string GetRandomString(int length, bool includeChars = true, bool includeDigits = true)
    {
      if (!includeChars && !includeDigits) throw new ArgumentException("includeChars or/and includeDigits must be true");

      string charPool = "";
      if (includeChars) charPool += "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
      if (includeDigits) charPool += "1234567890";

      StringBuilder rs = new StringBuilder();

      while (length-- > 0)
        rs.Append(charPool[(int)(ThreadSafeGetNextDouble() * charPool.Length)]);

      return rs.ToString();
    }

    internal static string GetHashString(string s)
    {
      // Create a new instance of the MD5CryptoServiceProvider object.
      MD5 md5Hasher = MD5.Create();

      // Convert the input string to a byte array and compute the hash.
      byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(s));

      // Create a new Stringbuilder to collect the bytes
      // and create a string.
      StringBuilder sBuilder = new StringBuilder();

      // Loop through each byte of the hashed data 
      // and format each one as a hexadecimal string.
      for (int i = 0; i < data.Length; i++)
      {
        sBuilder.Append(data[i].ToString("x2"));
      }

      // Return the hexadecimal string.
      return sBuilder.ToString();
    }

    public static string Agregate(string delimiter, params object[] objects)
    {
      if (objects == null) return "";
      objects = objects.Where(x => x != null).ToArray();
      objects = objects.Where(x => !(x is string) || !string.IsNullOrWhiteSpace(x as string)).ToArray();
      if (objects.Count() == 0) return "";

      return objects.Select(x => x.ToString()).Aggregate((c, v) => string.Format("{0}{1}{2}", c, delimiter, v));
    }
  }
}

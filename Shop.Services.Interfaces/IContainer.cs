using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Services.Interfaces
{
  public interface IContainer
  {
    void Register<T>(bool asSingleton = true) where T : class;
    void Register(Type type, bool asSingleton = true);
    void Register<T>(T instance) where T : class;
    void Register<T>(T instance, string name) where T : class;
    void Register(object instance);
    void Register<InterfaceType, ImplementationType>(bool asSingleton = true)
      where InterfaceType : class
      where ImplementationType : class, InterfaceType;
    void Register(Type type, Type implementationType, bool asSingleton = true);

    void Register<InterfaceType, ImplementationType>(string name, bool asSingleton = true)
      where InterfaceType : class
      where ImplementationType : class, InterfaceType;
    void Register(Type type, Type implementationType, string name, bool asSingleton = true);
    void RegisterMultiple<InterfaceType>(IEnumerable<Type> types, bool asSingleton = true)
      where InterfaceType : class;
    void RegisterMultiple(Type registrationType, IEnumerable<Type> implementationTypes, bool asSingleton = true);
    bool CanResolve<T>(string name) where T : class;
    bool CanResolve<T>() where T : class;
    T Resolve<T>() where T : class;
    IEnumerable<T> ResolveAll<T>() where T : class;
    object Resolve(Type t);
    T Resolve<T>(Dictionary<string, object> parametersOverloads) where T : class;
    T Resolve<T>(string name) where T : class;
  }
}

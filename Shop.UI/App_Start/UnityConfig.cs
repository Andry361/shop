using System;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Shop.Domain.Interfaces;
using Shop.Infrastructure.Data.Helpers;
using Shop.Services.Interfaces;
using Shop.Infrastructure.Business;
using Shop.Domain.Core;
using Shop.Infrastructure.Data.Repositories;
using Unity.WebApi;
using System.Web.Http;
using Shop.Services.Logic.Helpers;

namespace Shop.UI.App_Start
{
  /// <summary>
  /// Specifies the Unity configuration for the main container.
  /// </summary>
  public class UnityConfig
  {
    #region Unity Container
    private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
    {
      var container = new UnityContainer();
      RegisterTypes(container);
      return container;
    });

    /// <summary>
    /// Gets the configured Unity container.
    /// </summary>
    public static IUnityContainer GetConfiguredContainer()
    {
      return container.Value;
    }
    #endregion

    /// <summary>Registers the type mappings with the Unity container.</summary>
    /// <param name="container">The unity container to configure.</param>
    /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to 
    /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
    public static void RegisterTypes(IUnityContainer container)
    {
      // NOTE: To load from web.config uncomment the line below. Make sure to add a Microsoft.Practices.Unity.Configuration to the using statements.
      // container.LoadConfiguration();



      ////”брать это от сюда. ѕќдумать вообще на счет использовани€ unity
      //GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
      // TODO: Register your types here
      // container.RegisterType<IProductRepository, ProductRepository>();


      // register all your components with the container here
      // it is NOT necessary to register your controllers
      container.RegisterType<IUnitOfWork, UnitOfWork>(new PerRequestLifetimeManager());
      container.RegisterType<IRepository<User>, Repository<User>>();
      container.RegisterType<ITimeService, TimeService>();
      container.RegisterType<IUserService, UserService>();

      //container.RegisterType<IRepository<>, >();

    }
  }
}

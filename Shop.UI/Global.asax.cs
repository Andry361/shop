using Shop.UI.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using Unity.WebApi;

namespace Shop.UI
{
  public class WebApiApplication : System.Web.HttpApplication
  {
    protected void Application_Start()
    {
      GlobalConfiguration.Configure(WebApiConfig.Register);
      GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(UnityConfig.GetConfiguredContainer());
    }
  }
}

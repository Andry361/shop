using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Services.Interfaces
{
  public interface IContainerConfig
  {
    /// <summary>
    /// Perform registration actions for the whole application.
    /// Typically called once at startup.
    /// </summary>
    void RegisterGlobalServices(IContainer container);
    /// <summary>
    /// Register services and classes specific for the current session (e.g. a request in the web application)
    /// </summary>
    void RegisterSessionServices(IContainer container);
    void OnStartup();
  }
}

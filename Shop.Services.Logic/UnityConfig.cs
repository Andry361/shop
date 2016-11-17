using Shop.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Services.Logic
{
  public class UnityConfig : IContainerConfig
  {
    public void OnStartup()
    {
      throw new NotImplementedException();
    }

    public virtual void RegisterGlobalServices(IContainer container)
    {
      if (container == null)
        throw new ArgumentNullException("container");
      container.Register<IContainer>(container);
    }

    public virtual void RegisterSessionServices(IContainer container)
    {
      throw new NotImplementedException();
    }
  }
}

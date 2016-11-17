using Shop.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Infrastructure.Business
{
  public class UnityConfig : Shop.Services.Logic.UnityConfig
  {
    public override void RegisterGlobalServices(IContainer container)
    {
      if (container == null)
        throw new ArgumentNullException("container");
      base.RegisterGlobalServices(container);
    }

    public override void RegisterSessionServices(IContainer container)
    {
      if (container == null)
        throw new ArgumentNullException("container");
      base.RegisterSessionServices(container);

    }
  }
}

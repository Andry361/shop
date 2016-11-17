using FluentNHibernate.Automapping;
using FluentNHibernate.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Infrastructure.Data.Helpers
{
  public class AutomappingConfiguration : DefaultAutomappingConfiguration
  {
    public override bool ShouldMap(Type type)
    {
      return typeof(Shop.Domain.Core.Entity).IsAssignableFrom(type);
    }
  }
}

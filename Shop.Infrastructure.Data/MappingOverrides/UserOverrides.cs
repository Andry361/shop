using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;
using Shop.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Mapping.ByCode.Impl.CustomizersImpl;
namespace Shop.Infrastructure.Data.MappingOverrides
{

  public class UserOverrides : IAutoMappingOverride<User>
  {
    public void Override(AutoMapping<User> mapping)
    {
      mapping.Id(x => x.Id, "Id").GeneratedBy.GuidComb();
    }
  }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Core
{
  public abstract class BaseObject
  {
    public virtual Guid Id { get; set; }
    public virtual bool IsRemoved { get; set; }
    public virtual DateTime? CreationDateTime { get; set; }
    public virtual DateTime? LastChangeDateTime { get; set; }
    public virtual Guid CreatorId { get; set; }
    public virtual string CreatorName { get; set; }
    public virtual string DisplayName { get; set; }

    public override string ToString()
    {
      return DisplayName;
    }
  }
}

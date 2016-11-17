using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Services.Interfaces
{
  public interface ITimeService
  {
    DateTime Now { get; }
  }
}

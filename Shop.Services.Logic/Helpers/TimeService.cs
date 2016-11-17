using Shop.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Services.Logic.Helpers
{
  public class TimeService : ITimeService
  {
    public DateTime Now
    {
      get
      {
        return DateTime.UtcNow;
      }
    }
  }
}

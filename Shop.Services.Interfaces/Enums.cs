using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Services.Interfaces
{
  public static class Enums
  {
    public enum LoginResult
    {
      Rejected,
      UserBlocked,
      LoggedIn,
      SmsConfirmationCodeWasSent
    }
  }
}

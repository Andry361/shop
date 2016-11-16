using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Core
{
  public class User : Entity
  {
    public virtual string Login { get; set; }
    public virtual string PasswordHash { get; set; }
    public virtual string PasswordSalt { get; set; }
    public virtual bool IsAdmin { get; set; }
    public virtual string Name { get; set; }
    public virtual string Patronymic { get; set; }
    public virtual string Surname { get; set; }
    public virtual string Email { get; set; }
    public virtual string MobilePhoneNumber { get; set; }

  }
}

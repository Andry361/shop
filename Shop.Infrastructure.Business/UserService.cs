using Shop.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Domain.Core;
using Shop.Domain.Interfaces;

namespace Shop.Infrastructure.Business
{
  public class UserService : IUserService
  {
    private IRepository<User> _userRepository;

    public UserService(IRepository<User> userRepository)
    {
      if (userRepository == null)
        throw new ArgumentNullException("userRepository");
      _userRepository = userRepository;
    }
    public void Create(User product)
    {
      _userRepository.Create(product);
    }

    public void Delete(Guid id)
    {
      _userRepository.GetById(id);
    }

    public IList<User> GetAll()
    {
      return _userRepository
             .GetAll()
             .ToList();
    }

    public User GetById(Guid id)
    {
      return _userRepository.GetById(id);
    }

    public void Update(User product)
    {
      _userRepository.Update(product);
    }
  }
}

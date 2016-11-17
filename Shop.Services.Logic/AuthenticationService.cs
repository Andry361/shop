using Shop.Domain.Core;
using Shop.Domain.Interfaces;
using Shop.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Services.Logic
{
  //public class AuthenticationService : IAuthenticationService
  //{
  //  IRepository<User> _UsersRepository = null;
  //  Sessions.ISession _СurrentSession = null;
  //  ITimeService _TimeService = null;
  //  ISessionSmsConfirmationCodesStorage _SessionSmsConfirmationCodesStorage = null;

  //  public Guid? CurrentUserId
  //  {
  //    get
  //    {
  //      return _СurrentSession.UserId;
  //    }
  //  }

  //  public AuthenticationService(IRepository<User> usersRepository, IUserPasswordHashGenerator userPasswordHashGenerator,
  //                               ISmsSender smsSender, Sessions.ISession currentSession,
  //                               ITimeService timeService,
  //                               ISessionSmsConfirmationCodesStorage sessionSmsConfirmationCodesStorage)
  //  {
  //    if (usersRepository == null) throw new ArgumentNullException("usersRepository");
  //    if (userPasswordHashGenerator == null) throw new ArgumentNullException("userPasswordHashGenerator");
  //    if (smsSender == null) throw new ArgumentNullException("smsSender");
  //    if (currentSession == null) throw new ArgumentNullException("currentSession");
  //    if (sessionSmsConfirmationCodesStorage == null) throw new ArgumentNullException("sessionSmsConfirmationCodesStorage");
  //    if (timeService == null) throw new ArgumentNullException("timeService");

  //    _UsersRepository = usersRepository;
  //    _СurrentSession = currentSession;
  //    _SessionSmsConfirmationCodesStorage = sessionSmsConfirmationCodesStorage;
  //    _TimeService = timeService;
  //  }

  //  public LoginResult Login(string loginName, string password)
  //  {
  //    if (string.IsNullOrWhiteSpace(loginName)) throw new ValidationException(Properties.Resources.LoginNameIsNotSpecified);

  //    loginName = loginName.Trim().ToLower();
  //    var user = _UsersRepository.Query().Where(x => x.Login == loginName || x.Email == loginName).FirstOrDefault();
  //    if (user == null) return LoginResult.Rejected;
  //    if (user.IsBlocked) return LoginResult.UserBlocked;
  //    if (_UserPasswordHashGenerator.IsValidPassword(password, user.PasswordHash, user.PasswordSalt))
  //    {
  //      if (user.UseSmsLoginConfirmation)
  //      {
  //        var code = _SessionSmsConfirmationCodesStorage.GetConfirmationCode(_СurrentSession.SessionId).SmsConfirmationCode;
  //        _SmsSender.SendSms(user.MobilePhoneNumber, code);
  //        user.SmsLoginConfirmationCode = code;
  //        user.SmsLoginConfirmationCodeDateTime = _TimeService.Now;
  //        _UsersRepository.Save(user);
  //        _UsersRepository.Commit();

  //        return LoginResult.SmsConfirmationCodeWasSent;
  //      }
  //      else
  //      {
  //        _СurrentSession.UserId = user.Id;

  //        return LoginResult.LoggedIn;
  //      }
  //    }
  //    else
  //    {
  //      _СurrentSession.UserId = null;

  //      return LoginResult.Rejected;
  //    }
  //  }

  //  public void LogOut()
  //  {
  //    _СurrentSession.UserId = null;
  //  }

  //  public User CurrentUser
  //  {
  //    get
  //    {
  //      if (_СurrentSession.UserId == null) return null;

  //      return _UsersRepository.Load(_СurrentSession.UserId.Value);
  //    }
  //  }

  //  public void RequiresAuthentication()
  //  {
  //    if (!IsAuthenticated) throw new AccessDeniedException(AccessDeniedException.AccessDeniedReason.Unauthorized);
  //  }

  //  public bool IsAuthenticated
  //  {
  //    get
  //    {
  //      return this.CurrentUserId != null;
  //    }
  //  }
  //}
}

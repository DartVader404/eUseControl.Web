using eUseControl.BusinessLogic.Core;
using eUseControl.BusinessLogic.Interfaces;
using eUseControl.Domain.Entities.User;
using System.Web;

namespace eUseControl.BusinessLogic
{
    public class SessionBL : UserApi, ISession
    {
        public ULoginResp UserLogin(ULoginData data)
        {
            return UserLoginAction(data);
        }

        public HttpCookie GenCookie(string loginCredential)
        {
            return Cookie(loginCredential);
        }

        public URegisterResp UserRegister(URegisterData data)
        {
            return UserRegisterAction(data);
        }

        public UserMinimal GetUserByCookie(string apiCookieValue)
        {
            return UserCookie(apiCookieValue);
        }

        public URecoverResp RecoverPassword(string mail)
        {
            return RecoverPasswordAction(mail);
        }

        public UChangePassResp ChangePassword(UChangePassData data)
        {
            return ChangePasswordAction(data);
        }

    }
}

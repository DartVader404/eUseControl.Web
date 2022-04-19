using eUseControl.Domain.Entities.User;
using System.Web;

namespace eUseControl.BusinessLogic.Interfaces
{
    public interface ISession
    {
        ULoginResp UserLogin(ULoginData data);
        HttpCookie GenCookie(string loginCredential);
        URegisterResp UserRegister(URegisterData data);
        UserMinimal GetUserByCookie(string apiCookieValue);
        URecoverResp RecoverPassword(string mail);
        UChangePassResp ChangePassword(UChangePassData data);
    }
}

using AutoMapper;
using eUseControl.BusinessLogic.DBModel;
using eUseControl.Domain.Entities.User;
using eUseControl.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace eUseControl.BusinessLogic.Core
{
    public class UserApi : BaseApi
    {
        internal ULoginResp UserLoginAction(ULoginData data)
        {
            UDbTable result;
            var validate = new EmailAddressAttribute();

            if (validate.IsValid(data.Credential))
            {
                var pass = LoginHelper.HashGen(data.Password);
                using (var db = new UserContext())
                {
                    result = db.Users.FirstOrDefault(u => u.Email == data.Credential && u.Password == pass);
                }

                if (result == null)
                {
                    return new ULoginResp { Status = false, StatusMsg = "The Username or Password is Incorrect" };
                }

                using (var todo = new UserContext())
                {
                    result.LasIp = data.LoginIp;
                    result.LastLogin = data.LoginDateTime;
                    todo.Entry(result).State = EntityState.Modified;
                    todo.SaveChanges();
                }

                return new ULoginResp { Status = true };
            }
            else
            {
                var pass = LoginHelper.HashGen(data.Password);
                using (var db = new UserContext())
                {
                    result = db.Users.FirstOrDefault(u => u.UserName == data.Credential && u.Password == pass);
                }

                if (result == null)
                {
                    return new ULoginResp { Status = false, StatusMsg = "The Username or Password is Incorrect" };
                }

                using (var todo = new UserContext())
                {
                    result.LasIp = data.LoginIp;
                    result.LastLogin = data.LoginDateTime;
                    todo.Entry(result).State = EntityState.Modified;
                    todo.SaveChanges();
                }

                return new ULoginResp { Status = true };
            }
        }

        internal HttpCookie Cookie(string loginCredential)
        {
            var apiCookie = new HttpCookie("X-KEY")
            {
                Value = CookieGenerator.Create(loginCredential)
            };

            using (var db = new SessionContext())
            {
                Session curent;
                var validate = new EmailAddressAttribute();
                if (validate.IsValid(loginCredential))
                {
                    curent = (from e in db.Sessions where e.Credential == loginCredential select e).FirstOrDefault();
                }
                else
                {
                    curent = (from e in db.Sessions where e.Credential == loginCredential select e).FirstOrDefault();
                }

                if (curent != null)
                {
                    curent.CookieString = apiCookie.Value;
                    curent.ExpireTime = DateTime.Now.AddMinutes(60);
                    using (var todo = new SessionContext())
                    {
                        todo.Entry(curent).State = EntityState.Modified;
                        todo.SaveChanges();
                    }
                }
                else
                {
                    db.Sessions.Add(new Session
                    {
                        Credential = loginCredential,
                        CookieString = apiCookie.Value,
                        ExpireTime = DateTime.Now.AddMinutes(60)
                    });
                    db.SaveChanges();
                }
            }

            return apiCookie;
        }

        internal URegisterResp UserRegisterAction(URegisterData data)
        {

            UDbTable result;

            using (var db = new UserContext())
            {
                result = db.Users.FirstOrDefault(u => u.UserName == data.UserName || u.Email == data.Email);
            }

            if (result == null)
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<URegisterData, UDbTable>());
                var mapper = config.CreateMapper();
                UDbTable newUser = mapper.Map<UDbTable>(data);
                newUser.RegisterDate = DateTime.Now;
                newUser.CartProducts = 0;

                newUser.Password = LoginHelper.HashGen(newUser.Password);

                using (var db = new UserContext())
                {
                    db.Users.Add(newUser);
                    db.SaveChanges();
                }

            }
            else if (result.UserName == data.UserName)
            {
                return new URegisterResp { Status = false, StatusMsg = "Account with this Username already exist" };
            }
            else
            {
                return new URegisterResp { Status = false, StatusMsg = "Account with this Email already exist" };
            }

            return new URegisterResp() { Status = true };
        }

        internal UserMinimal UserCookie(string cookie)
        {
            Session session;
            UDbTable currentUser;

            using (var db = new SessionContext())
            {
                session = db.Sessions.FirstOrDefault(s => s.CookieString == cookie && s.ExpireTime > DateTime.Now);
            }

            if (session == null) return null;
            using (var db = new UserContext())
            {
                var validate = new EmailAddressAttribute();
                if (validate.IsValid(session.Credential))
                {
                    currentUser = db.Users.FirstOrDefault(u => u.Email == session.Credential);
                }
                else
                {
                    currentUser = db.Users.FirstOrDefault(u => u.UserName == session.Credential);
                }

                if (currentUser == null) return null;
                var config = new MapperConfiguration(cfg => cfg.CreateMap<UDbTable, UserMinimal>());
                var mapper = config.CreateMapper();
                UserMinimal userminimal = mapper.Map<UserMinimal>(currentUser);

                return userminimal;
            }
        }

        internal URecoverResp RecoverPasswordAction(string mail)
        {
            var validate = new EmailAddressAttribute();
            if (validate.IsValid(mail))
            {
                UDbTable user;
                using (var db = new UserContext())
                {
                    user = db.Users.FirstOrDefault(u => u.Email == mail);

                    if (user != null)
                    {
                        var newPassword = System.Web.Security.Membership.GeneratePassword(10, 3);
                        string msg = "Hi, " + user.UserName + "<br/>Your new password is " + newPassword + "<br/>You can login and change it in your Account!";
                        
                        if (MailHelper.SendMail(mail, "kelvinbear.one@gmail.com", msg, "New Password For Your Account !!!"))
                        {
                            user.Password = LoginHelper.HashGen(newPassword);
                            db.SaveChanges();
                            return new URecoverResp() { Status = true};
                        }
                        return new URecoverResp() { Status = false, StatusMsg = "Error send message!" };
                    }
                    else
                    {
                        return new URecoverResp() { Status = false, StatusMsg = "User not found!" };
                    }
                }
            }
            else
            {
                return new URecoverResp() { Status = false, StatusMsg = "Email is not valid!" };
            }
        }

        internal UChangePassResp ChangePasswordAction(UChangePassData data)
        {
            if (data.NewPassword == data.RepeatPassword)
            {
                UDbTable user;
                using (var db = new UserContext())
                {
                    user = db.Users.FirstOrDefault(m => m.Id == data.UserId);

                    if (user == null)
                    {
                        return new UChangePassResp() { Status = false, StatusMsg = "User not found!" };
                    }

                    if (user.Password == data.NewPassword)
                    {
                        return new UChangePassResp() { Status = false, StatusMsg = "New password can't be the same as the old one!" };
                    }

                    user.Password = LoginHelper.HashGen(data.NewPassword);
                    db.SaveChanges();
                    return new UChangePassResp() { Status = true};
                }
            }
            return new UChangePassResp() { Status = false, StatusMsg = "Passwords don't much!" };
        }

    }
}

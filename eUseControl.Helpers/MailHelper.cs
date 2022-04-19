using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace eUseControl.Helpers
{
    public class MailHelper
    {
        public static bool SendMail(string to, string from, string mailbody, string subject)
        {
            MailMessage message = new MailMessage(from, to);

            message.Subject = subject;
            message.Body = mailbody;

            message.BodyEncoding = Encoding.UTF8;
            message.IsBodyHtml = true;
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587); //Gmail smtp    
            System.Net.NetworkCredential basicCredential1 = new
            System.Net.NetworkCredential("kelvinbear.one@gmail.com", "Python_Flask_Mail_123");
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = basicCredential1;

            try
            {
                client.Send(message);
                return true;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

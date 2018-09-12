using System;
using System.Net;
using System.Net.Mail;

namespace Geo.Localization.Services.Utils
{
    public class SendEMail
    {
        public static string SendEmail(string receiverMail, string subject, string msgBody)
        {
            var msg = new MailMessage();

            msg.From = new MailAddress("martini.dev.architect@gmail.com");
            msg.To.Add(receiverMail);
            msg.Subject = subject;
            msg.IsBodyHtml = true;
            //"New Comments to Assign! " + DateTime.Now.ToString();
            msg.Body = msgBody;
            var client = new SmtpClient();
            client.UseDefaultCredentials = true;
            client.Host = "smtp.gmail.com";
            client.Port = 587;
            client.EnableSsl = true;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Credentials = new NetworkCredential("martini.dev.architect@gmail.com", "development123");
            client.Timeout = 20000;
            try
            {
                client.Send(msg);
                return "Mail has been successfully sent!";
            }
            catch (Exception ex)
            {
                return "Fail Has error" + ex.Message;
            }
            finally
            {
                msg.Dispose();
            }
        }
    }
}
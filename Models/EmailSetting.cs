using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace VendorAPI.Models
{
    public class EmailSetting
    {
        public string SMTPServer = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["SMTPServer"]);
        public string SMTPUserName = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["SMTPUserName"]);
        public string SMTPPassword = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["SMTPPassword"]);
        public string SMTPPort = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["SMTPPort"]);
        public string EmailFrom = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["EmailFrom"]);

        public bool SendMail(string To, string Cc, string subject, string Body)
        {
            bool status = false;
            try
            {
                MailMessage mm = new MailMessage(EmailFrom, To);
                mm.Subject = subject;
                mm.Body = Body;
                mm.IsBodyHtml = true;

                if (!string.IsNullOrEmpty(Cc))
                    mm.CC.Add(Cc);

                //if (!string.IsNullOrEmpty(Bcc))
                //    mm.Bcc.Add(Bcc);

                mm.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = SMTPServer;
                smtp.UseDefaultCredentials = false;

                System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
                NetworkCred.UserName = SMTPUserName;
                NetworkCred.Password = SMTPPassword;
                smtp.Credentials = NetworkCred;
                smtp.EnableSsl = true;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

                smtp.Port = Convert.ToInt32(SMTPPort);
                //if (smtpAtt != null)
                //{
                //    mm.Attachments.Add(smtpAtt);
                //}
                try
                {
                    smtp.Send(mm);
                    status = true;
                }
                catch (Exception ex)
                {                    
                    status = false;
                }
            }
            catch (Exception ex)
            {
                status = false;                
            }
            return status;
        }
    }
}
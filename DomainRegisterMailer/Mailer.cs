using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Security;
using System.ComponentModel;
using NLog;

namespace DomainRegisterMailer
{
    class Mailer : IMailer
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        MailAddress From = new MailAddress("noreply-alerts@animoassociates.com", "Domain Alerts");
        MailAddress ReplyTo = new MailAddress("andy.heeney@pcmann.co.uk");
        string InitialSubject = "Domain Renewal: ";
        string InitialBody = "<p>The following domains are due to be renewed.</p>" +
            "<p>Please could you indicate whether the client would like them renewed.</p>" +
            "<p>You may reply directly to this email; replies will be automatically sent to Andy Heeney.";
        SecureString SecurePassword { get; }

        public Mailer()
        {
            SecurePassword = new SecureString();
            SecurePassword.AppendChar('J');
            SecurePassword.AppendChar('o');
            SecurePassword.AppendChar('b');
            SecurePassword.AppendChar('o');
            SecurePassword.AppendChar('8');
            SecurePassword.AppendChar('6');
            SecurePassword.AppendChar('2');
            SecurePassword.AppendChar('1');
            SecurePassword.MakeReadOnly();
        }

        public void SendMail(List<DomainRenewalViewModel> domains)
        {
            ILookup<string, DomainRenewalViewModel> byHandler = domains.ToLookup(o => o.HandlerName);
            foreach (var handlerdomains in byHandler)
            {
                string body = string.Format("<p>Dear {0},</p> {1}", handlerdomains.Key.ToString(), InitialBody);
                string str = handlerdomains.Key.ToString();
                string subject = InitialSubject;
                foreach (var domain in handlerdomains)
                {
                    body += string.Format("<p>{0}, {1}, {2}</p>", domain.CompanyName, 
                        domain.DomainName, domain.RenewalDate.ToShortDateString());
                    subject += string.Format("{0} ", domain.DomainName);
                }

                body += "<p>Thank you<p>";

                MailMessage msg = new MailMessage();
                msg.To.Add(new MailAddress(handlerdomains.First().HandlerEmail, handlerdomains.Key));
                msg.From = From;
                msg.Subject = subject;
                msg.SubjectEncoding = Encoding.UTF8;
                msg.Body = body;
                msg.BodyEncoding = Encoding.UTF8;
                msg.IsBodyHtml = true;
                msg.ReplyToList.Add(ReplyTo);


                SmtpClient smtpClient = new SmtpClient("smtp.office365.com", 587);
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new System.Net.NetworkCredential(
                    "noreply-alerts@animoassociates.com", SecurePassword);
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.EnableSsl = true;
                smtpClient.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);
                smtpClient.SendAsync(msg, msg);
            }
        }

        private static void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {
            // Get the message we sent
            MailMessage msg = (MailMessage)e.UserState;

            if (e.Cancelled)
            {
                // TODO: Log cancelled email details
                Console.WriteLine("Email Cancelled: {0}, {1}", msg.To, e.Cancelled);
            }
            if (e.Error != null)
            {
                logger.Log(LogLevel.Error, string.Format("Email Error: {0}, {1}", msg.To, e.Error));
                Console.WriteLine("Email Error: {0}, {1}", msg.To, e.Error);
            }
            else
            {
                logger.Log(LogLevel.Info, string.Format("Email Success: {0}", msg.To));
                Console.WriteLine("Email Success: {0}", msg.To);
            }

            if (msg != null)
                msg.Dispose();
        }
    }
}

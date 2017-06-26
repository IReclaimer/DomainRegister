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
            "<p>You may reply directly to this email; replies will be automatically sent to Andy Heeney.</p>";
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

        public async Task SendMail(List<DomainRenewalViewModel> domains)
        {
            List<Task> mailQueue = new List<Task>();
            ILookup<string, DomainRenewalViewModel> byHandler = domains.ToLookup(o => o.HandlerName);
            foreach (var handlerdomains in byHandler)
            {
                string body = string.Format("<p>Dear {0},</p> {1}", handlerdomains.Key.ToString(), InitialBody);
                string subject = InitialSubject;
                string handlerEmailAddress = handlerdomains.First().HandlerEmail;
                foreach (var domain in handlerdomains)
                {
                    body += string.Format("<p>{0}, {1}, {2}</p>", domain.CompanyName, 
                        domain.DomainName, domain.RenewalDate.ToShortDateString());
                    subject += string.Format("{0} ", domain.DomainName);
                }

                body += "<p>Thank you<p>";

                Task mailTask = Task.Run(() => ActuallySendMailAsync(handlerEmailAddress, handlerdomains.Key.ToString(), subject, body));
                mailQueue.Add(mailTask);
            }
            Task.WaitAll(mailQueue.ToArray());
        }

        private async Task ActuallySendMailAsync(string mailTo, string mailName, string mailSubject, string mailBody)
        {
            using (MailMessage msg = new MailMessage())
            {
                msg.To.Add(new MailAddress(mailTo, mailName));
                msg.From = From;
                msg.Subject = mailSubject;
                msg.SubjectEncoding = Encoding.UTF8;
                msg.Body = mailBody;
                msg.BodyEncoding = Encoding.UTF8;
                msg.IsBodyHtml = true;
                msg.ReplyToList.Add(ReplyTo);

                using (SmtpClient smtpClient = new SmtpClient("smtp.office365.com", 587))
                {
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new System.Net.NetworkCredential(
                        "noreply-alerts@animoassociates.com", SecurePassword);
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtpClient.EnableSsl = true;
                    smtpClient.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);
                    await smtpClient.SendMailAsync(msg);
                }
            }
        }

        private static void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                logger.Log(LogLevel.Error, string.Format("Email Error: {0}", e.Error));
                Console.WriteLine("Email Error: {0}", e.Error);
            }
            else
            {
                logger.Log(LogLevel.Info, string.Format("Email Sent"));
                Console.WriteLine("Email Sent");
            }
        }
    }
}

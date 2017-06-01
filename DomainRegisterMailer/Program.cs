using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using DomainRegisterMailer.Models;
using System.Diagnostics;

namespace DomainRegisterMailer
{
    class Program
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {
            var p = new Program();
            IMailer mailer = new Mailer();
            p.DoMail(mailer);
            Console.ReadKey();
        }

        void DoMail(IMailer mailer)
        {
            List<DomainRenewalViewModel> domainsToBeRenewed = new List<DomainRenewalViewModel>();
            DateTime NintyDays = DateTime.Now.AddDays(60);
            using (var ctx = new DomainRegisterContext())
            {
                domainsToBeRenewed = ctx.Domains
                    .Where(d => d.RenewalDate < NintyDays)
                    .Select(d => new DomainRenewalViewModel
                    {
                        HandlerName = d.Handler.FirstName,
                        HandlerEmail = d.Handler.Email,
                        CompanyName = d.Company,
                        DomainName = d.DomainName,
                        RenewalDate = d.RenewalDate
                    })
                    .ToList<DomainRenewalViewModel>();
            }
            logger.Log(LogLevel.Info, string.Format("There are {0} domains to be renewed this week.", 
                domainsToBeRenewed.Count.ToString()));
            Console.WriteLine(string.Format("There are {0} domains to be renewed this week.",
                domainsToBeRenewed.Count.ToString()));
            mailer.SendMail(domainsToBeRenewed);
            logger.Log(LogLevel.Info, string.Format("Program runtime: {0}", (DateTime.UtcNow - Process.GetCurrentProcess().StartTime.ToUniversalTime()).ToString()));
            Console.WriteLine(string.Format("Program runtime: {0}", (DateTime.UtcNow - Process.GetCurrentProcess().StartTime.ToUniversalTime()).ToString()));
        }
    }
}

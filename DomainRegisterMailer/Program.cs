using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using DomainRegisterMailer.Models;
using System.Diagnostics;
using Nito.AsyncEx;

namespace DomainRegisterMailer
{
    class Program
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {
            AsyncContext.Run(() => MainAsync(args));

            logger.Log(LogLevel.Info, string.Format("Program runtime: {0}",
                (DateTime.UtcNow - Process.GetCurrentProcess().StartTime.ToUniversalTime()).ToString()));
            Console.WriteLine(string.Format("Program runtime: {0}",
                (DateTime.UtcNow - Process.GetCurrentProcess().StartTime.ToUniversalTime()).ToString()));

            Console.ReadKey();
        }

        static async void MainAsync(string[] args)
        {
            await new Program().DoMail(new Mailer());
        }

        async Task DoMail(IMailer mailer)
        {
            List<DomainRenewalViewModel> domainsToBeRenewed = new List<DomainRenewalViewModel>();
            DateTime warningPeriod = DateTime.Now.AddDays(60);

            using (var ctx = new DomainRegisterContext())
            {
                domainsToBeRenewed = ctx.Domains
                    .Where(d => d.RenewalDate < warningPeriod)
                    .Select(d => new DomainRenewalViewModel
                    {
                        HandlerName = d.Company.Handler.FirstName + " " + d.Company.Handler.LastName,
                        HandlerEmail = d.Company.Handler.Email,
                        CompanyName = d.Company.CompanyName,
                        DomainName = d.DomainName,
                        RenewalDate = d.RenewalDate
                    })
                    .ToList<DomainRenewalViewModel>();
            }

            logger.Log(LogLevel.Info, string.Format("There are {0} domains to be renewed this week.",
                domainsToBeRenewed.Count.ToString()));
            Console.WriteLine(string.Format("There are {0} domains to be renewed this week.",
                domainsToBeRenewed.Count.ToString()));

            if (domainsToBeRenewed.Count > 0)
                await mailer.SendMail(domainsToBeRenewed);

            logger.Log(LogLevel.Info, "All mail sent!");
            Console.WriteLine("All mail sent!");
        }
    }
}

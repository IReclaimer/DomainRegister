using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using DomainRegisterMailer.Models;
using Nito.AsyncEx;
using NLog;

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
            //Console.WriteLine(string.Format("Program runtime: {0}",
            //    (DateTime.UtcNow - Process.GetCurrentProcess().StartTime.ToUniversalTime()).ToString()));

            //Console.ReadKey();
        }

        static async void MainAsync(string[] args)
        {
            new Program().CheckForDomainRenewals(new Mailer());
        }

        void CheckForDomainRenewals(IMailer mailer)
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
            //Console.WriteLine(string.Format("There are {0} domains to be renewed this week.",
            //    domainsToBeRenewed.Count.ToString()));

            MailQueueResult mailResult = MailQueueResult.ProcessedSucessfully;

            if (domainsToBeRenewed.Count > 0)
                mailResult = mailer.SendMail(domainsToBeRenewed);

            switch (mailResult)
            {
                case MailQueueResult.TimedOut:
                    logger.Log(LogLevel.Error, "The mail queue timed out before completing.");
                    break;
                case MailQueueResult.TimedOutWithErrors:
                    logger.Log(LogLevel.Error, "The mail queue timed out before completing. " +
                        "Errors were encountered during the processing.");
                    break;
                case MailQueueResult.ProcessedWithErrors:
                    logger.Log(LogLevel.Error, "The mail queue completed processing with errors.");
                    break;
                default:
                    logger.Log(LogLevel.Info, "The mail queue completed processing sucessfully.");
                    break;
            }

            //logger.Log(LogLevel.Info, "All mail sent!");
            //Console.WriteLine("All mail sent!");
        }
    }
}

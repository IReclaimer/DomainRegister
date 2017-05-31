using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainRegisterMailer.Models;

namespace DomainRegisterMailer
{
    class DomainDBInitializer : DropCreateDatabaseAlways<DomainRegisterContext>
    {
        protected override void Seed(DomainRegisterContext context)
        {
            IList<Domain> defaultDomains = new List<Domain>();
            IList<Handler> defaultHandlers = new List<Handler>();

            defaultDomains.Add(new Domain()
            {
                Company = "Company 1",
                DomainName = "domain2",
                RenewalDate = DateTime.Now.AddDays(5)
            });

            defaultDomains.Add(new Domain()
            {
                Company = "Company 1",
                DomainName = "domain2",
                RenewalDate = DateTime.Now.AddDays(24)
            });

            defaultDomains.Add(new Domain()
            {
                Company = "Company 1",
                DomainName = "domain2",
                RenewalDate = DateTime.Now.AddDays(54)
            });

            defaultDomains.Add(new Domain()
            {
                Company = "Company 1",
                DomainName = "domain2",
                RenewalDate = DateTime.Now.AddDays(18)
            });

            defaultDomains.Add(new Domain()
            {
                Company = "Company 1",
                DomainName = "domain2",
                RenewalDate = DateTime.Now.AddDays(76)
            });

            defaultDomains.Add(new Domain()
            {
                Company = "Company 1",
                DomainName = "domain2",
                RenewalDate = DateTime.Now.AddDays(94)
            });

            defaultDomains.Add(new Domain()
            {
                Company = "Company 1",
                DomainName = "domain2",
                RenewalDate = DateTime.Now.AddDays(85)
            });

            defaultDomains.Add(new Domain()
            {
                Company = "Company 1",
                DomainName = "domain2",
                RenewalDate = DateTime.Now.AddDays(164)
            });

            defaultDomains.Add(new Domain()
            {
                Company = "Company 1",
                DomainName = "domain2",
                RenewalDate = DateTime.Now.AddDays(124)
            });

            defaultHandlers.Add(new Handler()
            {
                FirstName = "Nicole",
                LastName = "Froggat",
                Email = "administrator@animoassociates.com",
                Domains = new List<Domain>()
            });

            defaultHandlers.Add(new Handler()
            {
                FirstName = "Bianca",
                LastName = "Allen",
                Email = "administrator@animoassociates.com",
                Domains = new List<Domain>()
            });

            defaultHandlers.Add(new Handler()
            {
                FirstName = "Rebecca",
                LastName = "White",
                Email = "administrator@animoassociates.com",
                Domains = new List<Domain>()
            });

            defaultHandlers[0].Domains.Add(defaultDomains[0]);
            defaultHandlers[0].Domains.Add(defaultDomains[1]);
            defaultHandlers[0].Domains.Add(defaultDomains[2]);
            defaultHandlers[1].Domains.Add(defaultDomains[3]);
            defaultHandlers[1].Domains.Add(defaultDomains[4]);
            defaultHandlers[1].Domains.Add(defaultDomains[5]);
            defaultHandlers[2].Domains.Add(defaultDomains[6]);
            defaultHandlers[2].Domains.Add(defaultDomains[7]);
            defaultHandlers[2].Domains.Add(defaultDomains[8]);

            foreach (Domain domain in defaultDomains)
                context.Domains.Add(domain);
            foreach (Handler handler in defaultHandlers)
                context.Handlers.Add(handler);

            base.Seed(context);
        }
    }
}

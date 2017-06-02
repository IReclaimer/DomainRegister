using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainRegister.Models;

namespace DomainRegister
{
    class DomainDBInitializer : DropCreateDatabaseAlways<DomainRegisterContext>
    {
        protected override void Seed(DomainRegisterContext context)
        {
            IList<Domain> defaultDomains = new List<Domain>();
            IList<Company> defaultCompanies = new List<Company>();
            IList<Handler> defaultHandlers = new List<Handler>();

            defaultDomains.Add(new Domain()
            {
                DomainName = "domain2",
                RenewalDate = DateTime.Now.AddDays(5)
            });

            defaultDomains.Add(new Domain()
            {
                DomainName = "domain2",
                RenewalDate = DateTime.Now.AddDays(24)
            });

            defaultDomains.Add(new Domain()
            {
                DomainName = "domain2",
                RenewalDate = DateTime.Now.AddDays(54)
            });

            defaultDomains.Add(new Domain()
            {
                DomainName = "domain2",
                RenewalDate = DateTime.Now.AddDays(18)
            });

            defaultDomains.Add(new Domain()
            {
                DomainName = "domain2",
                RenewalDate = DateTime.Now.AddDays(76)
            });

            defaultDomains.Add(new Domain()
            {
                DomainName = "domain2",
                RenewalDate = DateTime.Now.AddDays(94)
            });

            defaultDomains.Add(new Domain()
            {
                DomainName = "domain2",
                RenewalDate = DateTime.Now.AddDays(85)
            });

            defaultDomains.Add(new Domain()
            {
                DomainName = "domain2",
                RenewalDate = DateTime.Now.AddDays(164)
            });

            defaultDomains.Add(new Domain()
            {
                DomainName = "domain2",
                RenewalDate = DateTime.Now.AddDays(124)
            });

            defaultCompanies.Add(new Company()
            {
                CompanyName = "Company 1",
                Domains = new List<Domain>()
            });

            defaultCompanies.Add(new Company()
            {
                CompanyName = "Company 2",
                Domains = new List<Domain>()
            });

            defaultCompanies.Add(new Company()
            {
                CompanyName = "Company 3",
                Domains = new List<Domain>()
            });

            defaultCompanies.Add(new Company()
            {
                CompanyName = "Company 4",
                Domains = new List<Domain>()
            });

            defaultCompanies.Add(new Company()
            {
                CompanyName = "Company 5",
                Domains = new List<Domain>()
            });

            defaultCompanies.Add(new Company()
            {
                CompanyName = "Company 6",
                Domains = new List<Domain>()
            });

            defaultHandlers.Add(new Handler()
            {
                FirstName = "Nicole",
                LastName = "Froggat",
                Email = "administrator@animoassociates.com",
                Companies = new List<Company>()
            });

            defaultHandlers.Add(new Handler()
            {
                FirstName = "Bianca",
                LastName = "Allen",
                Email = "administrator@animoassociates.com",
                Companies = new List<Company>()
            });

            defaultHandlers.Add(new Handler()
            {
                FirstName = "Rebecca",
                LastName = "White",
                Email = "administrator@animoassociates.com",
                Companies = new List<Company>()
            });

            defaultCompanies[0].Domains.Add(defaultDomains[0]);
            defaultCompanies[0].Domains.Add(defaultDomains[1]);
            defaultCompanies[1].Domains.Add(defaultDomains[2]);
            defaultCompanies[1].Domains.Add(defaultDomains[3]);
            defaultCompanies[2].Domains.Add(defaultDomains[4]);
            defaultCompanies[2].Domains.Add(defaultDomains[5]);
            defaultCompanies[3].Domains.Add(defaultDomains[6]);
            defaultCompanies[4].Domains.Add(defaultDomains[7]);
            defaultCompanies[5].Domains.Add(defaultDomains[8]);

            defaultHandlers[0].Companies.Add(defaultCompanies[0]);
            defaultHandlers[0].Companies.Add(defaultCompanies[1]);
            defaultHandlers[1].Companies.Add(defaultCompanies[2]);
            defaultHandlers[1].Companies.Add(defaultCompanies[3]);
            defaultHandlers[2].Companies.Add(defaultCompanies[4]);
            defaultHandlers[2].Companies.Add(defaultCompanies[5]);

            foreach (Domain domain in defaultDomains)
                context.Domains.Add(domain);
            foreach (Company company in defaultCompanies)
                context.Companies.Add(company);
            foreach (Handler handler in defaultHandlers)
                context.Handlers.Add(handler);

            base.Seed(context);
        }
    }
}

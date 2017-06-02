using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainRegister
{
    class DomainRenewalViewModel
    {
        public string HandlerName { get; set; }
        public string HandlerEmail { get; set; }
        public string CompanyName { get; set; }
        public string DomainName { get; set; }
        public DateTime RenewalDate { get; set; }
    }
}

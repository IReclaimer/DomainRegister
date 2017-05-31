using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainRegisterMailer
{
    interface IMailer
    {
        void SendMail(List<DomainRenewalViewModel> domains);
    }
}

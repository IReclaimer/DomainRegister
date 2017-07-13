using System.Collections.Generic;

namespace DomainRegisterMailer
{
    interface IMailer
    {
        MailQueueResult SendMail(List<DomainRenewalViewModel> domains);
    }
}

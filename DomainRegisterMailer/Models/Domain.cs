using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DomainRegisterMailer.Models
{
    public class Domain
    {
        // Primary key
        public int DomainId { get; set; }

        public string Company { get; set; }
        public string DomainName { get; set; }
        public DateTime RenewalDate { get; set; }

        // Foreign key
        public int HandlerId { get; set; }

        // Navigation property
        public virtual Handler Handler { get; set; }
    }
}
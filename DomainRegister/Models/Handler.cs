using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DomainRegister.Models
{
    public class Handler
    {
        // Primary key
        public int HandlerId { get; set; }

        public string HandlerName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        // Navigation property
        public virtual ICollection<Domain> Domains { get; set; }
    }
}
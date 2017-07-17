using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainRegister.Models
{
    interface ISoftDelete
    {
        Boolean IsDeleted { get; set; }
        DateTime? DeletedDate { get; set; }
    }
}

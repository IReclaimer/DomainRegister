using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DomainRegister.Models
{
    public class Company
    {
        // Primary Key
        public int CompanyId { get; set; }

        [Display(Name = "Company Name")]
        [MaxLength(70, ErrorMessage = "Company name cannot be greater than 70 characters.")]
        [Required]
        public string CompanyName { get; set; }

        // Foreign Key
        [Display(Name = "File Handler")]
        public int HandlerId { get; set; }

        // Navigation property
        public virtual ICollection<Domain> Domains { get; set; }
        public virtual Handler Handler { get; set; }
    }
}
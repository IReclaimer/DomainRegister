using System;
using System.ComponentModel.DataAnnotations;

namespace DomainRegister.Models
{
    public class Domain
    {
        // Primary key
        public int DomainId { get; set; }

        [Display(Name="Domain Name")]
        [MaxLength(50, ErrorMessage = "Domain name cannot be greater than 50 characters.")]
        [Required]
        public string DomainName { get; set; }
        [Display(Name="Renewal Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Required]
        public DateTime RenewalDate { get; set; }

        // Foreign key
        [Display(Name = "Company")]
        public int CompanyId { get; set; }

        // Navigation property
        public virtual Company Company { get; set; }
    }
}
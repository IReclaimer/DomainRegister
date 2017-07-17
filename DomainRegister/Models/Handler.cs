using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DomainRegister.Models
{
    public class Handler : ISoftDelete
    {
        // Primary key
        public int HandlerId { get; set; }
        
        [MaxLength(15, ErrorMessage ="First name cannot be longer than 15 characters.")]
        [Display(Name ="First Name")]
        [Required]
        public string FirstName { get; set; }
        [MaxLength(15, ErrorMessage = "Last name cannot be longer than 15 characters.")]
        [Display(Name = "Last Name")]
        [Required]
        public string LastName { get; set; }
        [MaxLength(50, ErrorMessage = "Email address cannot be longer than 50 characters.")]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", 
            ErrorMessage = "Not a valid email address.")]
        [Required]
        public string Email { get; set; }

        //ISoftDelete implementation
        [ScaffoldColumn(false)]
        public Boolean IsDeleted { get; set; }
        [ScaffoldColumn(false)]
        [Display(Name = "Date Deleted")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? DeletedDate { get; set; }

        // Navigation property
        public virtual ICollection<Company> Companies { get; set; }
    }
}
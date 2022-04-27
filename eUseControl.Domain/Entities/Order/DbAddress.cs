using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUseControl.Domain.Entities.User
{
    public class DbAddress
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AddressId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        [Display(Name = "FirstName")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "LastName")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Country")]
        public string Country { get; set; }
        
        [Required]
        [Display(Name = "State")]
        public string State { get; set; }
        
        [Required]
        [Display(Name = "City")]
        public string City { get; set; }

        [Required]
        [Display(Name = "StreetAddress")]
        public string StreetAddress { get; set; }

        [Required]
        [Display(Name = "PostCode")]
        public string PostCode { get; set; }

        [Required]
        [Display(Name = "Phone")]
        public string Phone { get; set; }

        [Required]
        [Display(Name = "Email Address")]
        [StringLength(30)]
        public string Email { get; set; }

        [DataType(DataType.Date)]
        public DateTime AddedDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime LastEdit { get; set; }

    }
}

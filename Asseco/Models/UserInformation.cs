using System.ComponentModel.DataAnnotations;

namespace Asseco.Models
{
    public class UserInformation
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Lastname { get; set; }

        [Required]
        public string Email { get; set; }


        
        public string? Street { get; set; }
        public string? Suite { get; set; }
        public string? City { get; set; }
        public int? Zipcode { get; set; }
        public int? Lat { get; set; }
        public int? Lng { get; set; }


        public int? Phone { get; set; }
        public string? Website { get; set; }

        [Display(Name= "Company Name")]
        public string? CompanyName { get; set; }
        [Display(Name = "Company Catch Phrase")]
        public string? CompanyCatchPhrase { get; set; }
        public string? Bs { get; set; }


    }
}

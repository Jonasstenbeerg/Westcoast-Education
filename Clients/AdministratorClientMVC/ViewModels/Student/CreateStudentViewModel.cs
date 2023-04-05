using System.ComponentModel.DataAnnotations;

namespace AdministratorClientMVC.ViewModels.Student
{
    public class CreateStudentViewModel
    {   [Display(Name ="Förnamn")]
        [Required(ErrorMessage ="Förnamn är obligatoriskt")]
        [RegularExpression(@"^[A-Öa-ö]*$",ErrorMessage ="Bara bokstäver kan anges")]
        public string? FirstName { get; set; }
        [Display(Name ="Efternamn")]
        [Required(ErrorMessage ="Efternamn är obligatoriskt ")]
        [RegularExpression(@"^[A-Öa-ö]*$",ErrorMessage ="Bara bokstäver kan anges")]
        public string? LastName { get; set; }
        [Display(Name ="Email")]
        [Required(ErrorMessage ="Email är obligatoriskt")]
        [EmailAddress(ErrorMessage = "Felaktig email")]
        public string? Email { get; set; }
        [Display(Name ="Telefonnummer")]
        [Required(ErrorMessage ="Telefonnummer är obligatoriskt")]
        [RegularExpression(@"^[0-9]*$",ErrorMessage ="Bara siffror kan anges")]
        public string? PhoneNumber { get; set; }
        [Display(Name ="Gatunamn")]
        [Required(ErrorMessage ="Gatunamn är obligatoriskt")]
        [RegularExpression(@"^[A-Öa-ö\s]*$",ErrorMessage ="Bara bokstäver kan anges")]
        public string? StreetName { get; set; }
        [Display(Name ="Gatunummer")]
        [Required(ErrorMessage ="Gatunummer är obligatoriskt")]
        [RegularExpression(@"^[0-9]*$",ErrorMessage ="Bara siffror kan anges")]
        [Range(1,950,ErrorMessage ="Bara nummer från 1 till 950 tas emot")]
       
        public string? StreetNumber { get; set; }
        [Display(Name ="Postnummer")]
        [Required(ErrorMessage ="Postnummer är obligatoriskt")]
        [RegularExpression(@"^[0-9]*$",ErrorMessage ="Bara siffror kan anges")]
        [Range(1,75593, ErrorMessage ="Ett giltigt postnummer är från 1 till 75593")]
        public string? Zipcode { get; set; }
    }
}
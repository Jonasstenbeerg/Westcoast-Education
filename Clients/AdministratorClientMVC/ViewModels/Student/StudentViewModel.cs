using System.ComponentModel.DataAnnotations;

namespace AdministratorClientMVC.ViewModels.Student
{
    public class StudentViewModel
    {
        
         public int StudentId { get; set; }
        [Display(Name="Förnamn")]
        [Required(ErrorMessage ="Förnamn är obligatoriskt")]
        public string? FirstName { get; set; }
        [Display(Name="Efternamn")]
        [Required(ErrorMessage ="Efternamn är obligatoriskt")]
        public string? LastName { get; set; }
        [Display(Name="Epost")]
        [Required(ErrorMessage ="Epost är obligatoriskt")]
        public string? Email { get; set; }
        [Display(Name="Telefonnummer")]
        [Required(ErrorMessage ="Telefonnummer är obligatoriskt")]
        public string? PhoneNumber { get; set; }
        [Display(Name="Gatunamn")]
        [Required(ErrorMessage ="Gatunamn är obligatoriskt")]
        public string? StreetName { get; set; }
        [Display(Name="Gatunummer")]
        [Required(ErrorMessage ="Gatunummer är obligatoriskt")]
        public int StreetNumber { get; set; }
        [Display(Name="Postadress")]
        [Required(ErrorMessage ="Postadress är obligatoriskt")]
        public int Zipcode { get; set; }
    }
}
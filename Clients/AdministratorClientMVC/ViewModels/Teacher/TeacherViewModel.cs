using System.ComponentModel.DataAnnotations;

namespace AdministratorClientMVC.ViewModels.Teacher
{
    public class TeacherViewModel
    {
        [Display(Name="LärarId")]
         public int TeacherId { get; set; }
         [Display(Name="Förnamn")]
        public string? FirstName { get; set; }
        [Display(Name="Efternamn")]
        public string? LastName { get; set; }
        [Display(Name="Epost")]
        public string? Email { get; set; }
        [Display(Name="Telefonnummer")]
        public string? PhoneNumber { get; set; }
        [Display(Name="Gatunamn")]
        public string? StreetName { get; set; }
        [Display(Name="Gatunummer")]
        public int StreetNumber { get; set; }
        [Display(Name="Postnummer")]
        public int Zipcode { get; set; }
    }
}
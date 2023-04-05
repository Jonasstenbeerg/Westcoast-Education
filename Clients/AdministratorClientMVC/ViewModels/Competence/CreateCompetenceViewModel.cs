using System.ComponentModel.DataAnnotations;

namespace AdministratorClientMVC.ViewModels
{
    public class CreateCompetenceViewModel
    {
        [Display(Name="Kompetens")]
        [Required(ErrorMessage ="Kompetens är obligatoriskt")]
         public string? CompetenceName { get; set; }
                
    }
}
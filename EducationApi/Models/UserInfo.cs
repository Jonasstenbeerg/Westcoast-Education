using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EducationApi.Models
{
    public class UserInfo
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="Förnamn är obligatoriskt")]
        public string? FirstName { get; set; }
        [Required(ErrorMessage ="Efternamn är obligatoriskt")]
        public string? LastName { get; set; }
        [Required(ErrorMessage ="Email är obligatoriskt")]
        [EmailAddress(ErrorMessage = "Felaktig inmatning")]
        public string? Email { get; set; }
        [Required(ErrorMessage ="Telefonnummer är obligatoriskt")]
        [Phone(ErrorMessage ="Felaktig inmatning")]
        public string? PhoneNumber { get; set; }
        public int AddressId { get; set; }
        //Navprop
        public Teacher? TeacherInfo { get; set; }
        public Student? StudentInfo { get; set; }
        [ForeignKey("AddressId")]
        public Address Address { get; set; } = new Address();

        
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EducationApi.Models
{
    public class Address
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? StreetName { get; set; }
        [Required]
        public int StreetNumber { get; set; }
        [Required]
        public int Zipcode { get; set; }
        
        public ICollection<UserInfo> Users { get; set; }= new List<UserInfo>();

    }

    
}
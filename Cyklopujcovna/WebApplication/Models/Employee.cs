using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Jméno")]
        [Required(ErrorMessage ="Jméno musí být zadáno!")]
        public string Name { get; set; }
        [DisplayName("Heslo")]
        [Required(ErrorMessage ="Heslo musí být zadáno!")]
        public string Passwd { get; set; }
    }
}

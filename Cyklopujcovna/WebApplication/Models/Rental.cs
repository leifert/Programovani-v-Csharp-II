using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public class Rental
    {
        [Key]
        public int  Id { get; set; }
        [DisplayName("Kolo")]
        [Required(ErrorMessage ="Kolo musí být vybráno!")]
        public int  BikeId { get; set; }
        [DisplayName("Začátek výpůjčky")]
        [Required(ErrorMessage ="Začátek výpůjčky musí být zadán!")]
        public DateTime Start { get; set; }
        [DisplayName("Konec výpůjčky")]
        [Required(ErrorMessage ="Konec výpůjčky musí být zadán!")]
        public DateTime End { get; set; }
        [DisplayName("Cena výpůjčky")]
        [Required(ErrorMessage ="Cena výpůjčky musí být zadána!")]
        public double Price { get; set; }
        [DisplayName("Jméno zákazníka")]
        [Required(ErrorMessage ="Jméno zákazníka musí být zadáno!")]
        public string CustomerName { get; set; }
        [DisplayName("Email zákazníka")]
        [Required(ErrorMessage ="Email zákazníka musí být zadán!")]
        [EmailAddress]
        public string CustomerEmail { get; set; }
        [DisplayName("Telefon zákazníka")]
        [Required(ErrorMessage ="Telefon zákazníka musí být zadán!")]
        public string CustomerPhoneNumber { get; set; }
    }
}

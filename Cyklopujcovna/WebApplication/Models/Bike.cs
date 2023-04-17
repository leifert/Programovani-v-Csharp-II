using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public class Bike
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Název kola")]
        [Required(ErrorMessage ="Název kola musí být zadán!")]
        public string BikeName { get; set; }
        [DisplayName("Typ kola")]
        [Required(ErrorMessage ="Typ kola musí být zadán!")]
        public string BikeType { get; set; }

        [DisplayName("Cena kola")]
        [Required(ErrorMessage ="Cena kola musí být zadána!")]
        public double BikePrice { get; set; }
    }
    public class KeyAttribute : Attribute
    {
    }
}

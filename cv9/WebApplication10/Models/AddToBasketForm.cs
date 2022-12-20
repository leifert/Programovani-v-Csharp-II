using System.ComponentModel.DataAnnotations;

namespace WebApplication10.Models
{
    public class AddToBasketForm
    {
        [Required]
        public int? ProductId { get; set; }

        [Display(Name = "Počet")]
        [Required(ErrorMessage = "Počet kusů je povinný.")]
        //[Range(2, 10)]
        //[EmailAddress]
        public int? Count { get; set; }
    }
}

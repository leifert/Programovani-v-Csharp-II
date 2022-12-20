using System.ComponentModel.DataAnnotations;

namespace WebApplication10.Models
{
    public class OrderForm
    {
        [Display(Name = "Jméno")]
        [Required]
        public string FirstName { get; set; }

        [Display(Name = "Příjmení")]
        [Required]
        public string LastName { get; set; }


        [Display(Name = "Email")]
        [Required]
        [EmailAddress]
        public string Email { get; set; }


        [Display(Name = "Způsob doručení")]
        [Required]
        public int? Delivery { get; set; }
    }
}

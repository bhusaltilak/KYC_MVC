using System.ComponentModel.DataAnnotations;

namespace KYC_MVC.Models
{
    public class KYC
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        [Required(ErrorMessage = "Phone Number is required")]
        [RegularExpression(@"^\d{10,15}$", ErrorMessage = "Phone number must be between 10 and 15 digits")]
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Province { get; set; }
        public string District { get; set; }
        public string VDC { get; set; }
    }
}
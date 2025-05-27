using System.ComponentModel.DataAnnotations;


namespace LifeDrop.ViewModels
{
    public class DonationViewModel
    {
        [Required]
        public string? FullName { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        public string? BloodGroup { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DonationDate { get; set; }

        [Required]
        public string? Location { get; set; }

        public string? Notes { get; set; }
    }
}

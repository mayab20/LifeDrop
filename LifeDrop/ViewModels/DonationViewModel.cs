using System.ComponentModel.DataAnnotations;

namespace LifeDrop.ViewModels
{
    public class DonationViewModel
    {
        // For display only
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? BloodGroup { get; set; }

        // For user input
        [Required, DataType(DataType.Date)]
        public DateTime DonationDate { get; set; }

        public required string Location { get; set; }

        public string? Notes { get; set; }
    }
}

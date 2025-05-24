using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class User
{
    public int Id { get; set; }

    [Required(ErrorMessage = "FirstName is required")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "LastName is required")]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Username is required")]
    [StringLength(20, MinimumLength = 4, ErrorMessage = "Username must be between 4 and 20 characters")]
    public string Username { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;

    [NotMapped] // This field is not stored in the DB
    [Compare("Password", ErrorMessage = "Passwords do not match")]
    [DataType(DataType.Password)]
    public string ConfirmPassword { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Blood type is required")]
    [RegularExpression(@"^(A|B|AB|O)[+-]$", ErrorMessage = "Invalid blood type")]
    public string BloodType { get; set; } = string.Empty;

    [Required(ErrorMessage = "Tel number is required")]
    [Phone(ErrorMessage = "Invalid telephone number")]
    public string TelNumber { get; set; } = string.Empty;

    [Required(ErrorMessage = "Gender is required")]
    public string Gender { get; set; } = string.Empty;

    [Required(ErrorMessage = "Date of birth is required")]
    [DataType(DataType.Date)]
    public DateTime DateOfBirth { get; set; }

    [Required(ErrorMessage = "Location is required")]
    public string Location { get; set; } = string.Empty;

    [Required(ErrorMessage = "Last Donation Date is required")]
    [DataType(DataType.Date)]
    public DateTime LastDonationDate { get; set; } 

}

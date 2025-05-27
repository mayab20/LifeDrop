using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class User
{
    [Key]
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

    public virtual ICollection<Donation> donationDonors { get; set; } = new List<Donation>();

    public virtual ICollection<Donation> donationRecipients { get; set; } = new List<Donation>();

}



public partial class Donation
{
    public int DonationId { get; set; }

    public int DonorId { get; set; }

    public DateOnly DonationDate { get; set; }

    public string DonationLocation { get; set; } = null!;

    public int RecipientId { get; set; }

    public virtual user Donor { get; set; } = null!;

    public virtual user Recipient { get; set; } = null!;
}



public class DonationCenter
{
    [Key]
    public int CenterId { get; set; }

    [Required]
    [StringLength(100, ErrorMessage = "Center name cannot exceed 100 characters.")]
    public string CenterName { get; set; }

    [Required]
    [StringLength(200, ErrorMessage = "Address cannot exceed 200 characters.")]
    public string Address { get; set; }

    [Required]
    [StringLength(100, ErrorMessage = "City name cannot exceed 100 characters.")]
    public string City { get; set; }

    [Required]
    [Phone(ErrorMessage = "Invalid contact number format.")]
    [StringLength(15)]
    public string ContactNumber { get; set; }
}


public class BloodInventory
{
  [Key]
  public int InventoryId { get; set; }

  [Required]
  [ForeignKey("DonationCenter")]
  public int CenterId { get; set; }

  [Required]
  [StringLength(3)] 
  [RegularExpression(@"^(A|B|AB|O)[+-]$", ErrorMessage = "Invalid blood type")]
  public string BloodType { get; set; }

  [Required]
  public int UnitsAvailable { get; set; }

  public DonationCenter Center { get; set; }
}


public class Appointment
{
    [Key]
    public int AppointmentId { get; set; }

    [Required]
    [ForeignKey("User")]
    public int UserId { get; set; }

    [Required]
    [ForeignKey("Center")]
    public int CenterId { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime AppointmentDate { get; set; }

    [Required]
    public TimeSpan TimeSlot { get; set; }

    [Required]
    [StringLength(20, ErrorMessage = "Status must be 20 characters or fewer.")]
    [RegularExpression("^(Scheduled|Completed|Cancelled)$", ErrorMessage = "Status must be Scheduled, Completed, or Cancelled.")]
    public string Status { get; set; }

    public User User { get; set; }
    public DonationCenter Center { get; set; }
}


public class LoginViewModel
{
    [Required]
    public required string Username { get; set; }

    [Required, DataType(DataType.Password)]
    public required string Password { get; set; }
}




public class RegisterViewModel
{
    [Required]
    public required string Username { get; set; }

    [Required, EmailAddress]
    public required string Email { get; set; }

    [Required, DataType(DataType.Password)]
    public required string Password { get; set; }

    [Compare("Password", ErrorMessage = "Passwords do not match")]
    [DataType(DataType.Password)]
    public required string ConfirmPassword { get; set; }
}

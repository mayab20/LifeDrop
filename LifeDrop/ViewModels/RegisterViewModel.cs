using System;
using System.ComponentModel.DataAnnotations;

public class RegisterViewModel
{
  [Required]
  public required string FirstName { get; set; }

  [Required]
  public required string LastName { get; set; }

  [Required]
  public required string Username { get; set; }

  [Required, EmailAddress]
  public required string Email { get; set; }

  [Required]
  public required string TelNumber { get; set; }

  [Required]
  public required string Gender { get; set; }

  [Required, DataType(DataType.Date)]
  public DateTime? DateOfBirth { get; set; }

  [Required]
  public required string BloodGroup { get; set; }

  [Required]
  public required string Location { get; set; }

  [Required, DataType(DataType.Password)]
  public required string Password { get; set; }

  [Required, Compare("Password")]
  public required string ConfirmPassword { get; set; }
    
  public DateOnly? LastDonationDate { get; set; }

}

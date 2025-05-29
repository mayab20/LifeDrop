namespace LifeDrop.ViewModels;

public class ProfileViewModel
{
    public required bool IsAdmin { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Username { get; set; }
    public required string Email { get; set; }
    public required string TelNumber { get; set; }
    public required string Gender { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public required string BloodGroup { get; set; }
    public required string Location { get; set; }
    public DateTime? LastDonationDate { get; set; }
    public int DonationCount { get; set; }
}

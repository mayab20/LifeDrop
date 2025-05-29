using System;
using System.Collections.Generic;

namespace LifeDrop.Models;

public partial class User
{
    public int Id { get; set; }

    public bool IsAdmin { get; set; }

    public bool IsRestricted { get; set; } = false;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string BloodGroup { get; set; } = null!;

    public string TelNumber { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public DateOnly DateOfBirth { get; set; }

    public string Location { get; set; } = null!;

    public DateTime? LastDonationDate { get; set; }

    public int DonationCount { get; set; } = 0;


    public virtual ICollection<Appointment> appointments { get; set; } = new List<Appointment>();

    public virtual ICollection<Donation> donationDonors { get; set; } = new List<Donation>();

    public virtual ICollection<Donation> donationRecipients { get; set; } = new List<Donation>();

}

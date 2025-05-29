using System;
using System.Collections.Generic;

namespace LifeDrop.Models;

public partial class Donation
{
    public int DonationId { get; set; }

    public int DonorId { get; set; }

    public DateOnly DonationDate { get; set; }

    public string DonationLocation { get; set; } = null!;

    public int RecipientId { get; set; }
    public bool IsConfirmed { get; set; } = false;

    public virtual User Donor { get; set; } = null!;

    public virtual User Recipient { get; set; } = null!;

    

}

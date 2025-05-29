using System;
using System.Collections.Generic;

namespace LifeDrop.Models;

public partial class DonationCenter
{
    public int CenterId { get; set; }

    public string CenterName { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string City { get; set; } = null!;

    public string ContactNumber { get; set; } = null!;

    public virtual ICollection<Appointment> appointments { get; set; } = new List<Appointment>();

    public virtual ICollection<BloodInventory> bloodinventories { get; set; } = new List<BloodInventory>();


}

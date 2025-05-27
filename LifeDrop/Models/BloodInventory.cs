using System;
using System.Collections.Generic;

namespace LifeDrop.Models;

public partial class Bloodinventory
{
    public int InventoryId { get; set; }

    public int CenterId { get; set; }

    public string BloodType { get; set; } = null!;

    public int UnitsAvailable { get; set; }
    public virtual DonationCenter Center { get; set; } = null!;
}
